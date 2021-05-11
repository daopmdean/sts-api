using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Enums;
using Service.Exceptions;
using Service.Interfaces;
using Service.Models;

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

        public async Task<UserReturn> Login(LoginInfo info)
        {
            User user = await _context.Users
                .SingleOrDefaultAsync(x => x.Username == info.Username.ToLower());

            if (user == null)
            {
                throw new AppException(
                    (int)StatusCode.Unauthorized, "Invalid username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(info.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i])
                    throw new AppException(
                        (int)StatusCode.Unauthorized, "Invalid Password");
            }

            return new UserReturn
            {
                Status = 200,
                Username = user.Username,
                Token = _tokenService.GenerateToken(user)
            };
        }

        public async Task<UserReturn> Register(RegisterInfo info)
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

            return new UserReturn
            {
                Status = 200,
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
