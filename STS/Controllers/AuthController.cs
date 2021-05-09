using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace STS.Controllers
{
    public class AuthController : ApiBaseController
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterInfo info)
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

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginInfo info)
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

            return Ok(user);
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(
                x => x.Username == username.ToLower());
        }
    }
}
