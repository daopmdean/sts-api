using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Data.Entities;
using Data.Enums;
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
        private readonly IMapper _mapper;

        public AuthService(DataContext context,
            ITokenService tokenService, IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<UserTokenResponse> Login(LoginRequest login)
        {
            User user = await _context.Users
                .Include(user => user.Role)
                .SingleOrDefaultAsync(x =>
                    x.Username == login.Username.ToLower()
                    && x.Status == Status.Active);

            if (user == null)
            {
                throw new AppException(
                    (int)StatusCode.UnAuthorized, "Invalid username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i])
                    throw new AppException(
                        (int)StatusCode.UnAuthorized, "Invalid Password");
            }

            return new UserTokenResponse
            {
                Status = (int)StatusCode.Ok,
                Username = user.Username,
                Token = _tokenService.GenerateToken(user)
            };
        }

        public async Task<UserTokenResponse> Register(RegisterRequest info)
        {
            if (await UserExist(info.Username))
                throw new AppException(400, "Username already exist");

            using var hmac = new HMACSHA512();

            var user = _mapper.Map<User>(info);
            user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(info.Password));
            user.PasswordSalt = hmac.Key;
            user.RoleId = (int)UserRole.BrandManager;

            _context.Users.Add(user);

            if (await _context.SaveChangesAsync() <= 0)
                throw new AppException(400, "Can not register user");

            return new UserTokenResponse
            {
                Status = (int)StatusCode.Ok,
                Username = user.Username,
                Token = _tokenService.GenerateToken(user)
            };
        }

        public async Task<UserTokenResponse> RegisterWithRole(int roleId,
            RegisterRequest info)
        {
            if (await UserExist(info.Username))
                throw new AppException(400, "Username already exist");

            using var hmac = new HMACSHA512();

            var user = _mapper.Map<User>(info);
            user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(info.Password));
            user.PasswordSalt = hmac.Key;
            user.RoleId = roleId;

            _context.Users.Add(user);

            if (await _context.SaveChangesAsync() <= 0)
                throw new AppException(400, "Can not register user");

            return new UserTokenResponse
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
