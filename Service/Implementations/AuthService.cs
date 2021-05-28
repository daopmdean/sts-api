using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Service.Enums;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<UserResponse> Login(LoginRequest info)
        {
            User user = await _context.Users
                .SingleOrDefaultAsync(x => x.Username == info.Username.ToLower());

            if (user == null)
            {
                throw new AppException(
                    (int)StatusCode.UnAuthorized, "Invalid username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(info.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i])
                    throw new AppException(
                        (int)StatusCode.UnAuthorized, "Invalid Password");
            }

            return new UserResponse
            {
                Status = (int)StatusCode.Ok,
                Username = user.Username,
                Token = _tokenService.GenerateToken(user)
            };
        }

        public async Task<UserResponse> Register(RegisterRequest info)
        {
            if (await UserExist(info.Username))
            {
                throw new AppException(400, "Username already exist");
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = info.Username.ToLower(),
                FirstName = info.FirstName,
                LastName = info.LastName,
                Email = info.Email,
                Address = info.Address,
                Phone = info.Phone,
                DateOfBirth = info.DateOfBirth,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(info.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponse
            {
                Status = (int)StatusCode.Ok,
                Username = user.Username,
                Token = _tokenService.GenerateToken(user)
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(
                user => user.Username == username.ToLower());
        }
    }
}
