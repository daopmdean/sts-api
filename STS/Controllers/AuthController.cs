using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Models;

namespace STS.Controllers
{
    public class AuthController : ApiBaseController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserReturn>> Register(RegisterInfo info)
        {
            if (await UserExist(info.Username))
            {
                return BadRequest("Username already exist!");
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = info.Username.ToLower(),
                FirstName = info.FirstName,
                LastName = info.LastName,
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

        [HttpPost("login")]
        public async Task<ActionResult<UserReturn>> Login(LoginInfo info)
        {
            User user = await _context.Users
                .SingleOrDefaultAsync(x => x.Username == info.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(info.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i])
                    return Unauthorized("Invalid Password");
            }

            return new UserReturn
            {
                Status = 200,
                Username = user.Username,
                Token = _tokenService.GenerateToken(user)
            }; ;
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(
                x => x.Username == username.ToLower());
        }
    }
}
