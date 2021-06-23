using System;
using Data.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Service.Enums;
using Data.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IStoreStaffRepository _storeStaffRepo;

        public TokenService(
            IConfiguration config,
            IStoreStaffRepository storeStaffRepo)
        {
            _key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["TokenKey"]));
            _storeStaffRepo = storeStaffRepo;
        }

        public async Task<string> GenerateToken(User user)
        {
            string storeId = "";

            if (user.RoleId == (int)UserRole.StoreManager)
            {
                int storeIdInt = await _storeStaffRepo
                    .GetStoreId(user.Username);

                if (storeIdInt > 0)
                {
                    storeId = storeIdInt.ToString();
                }
                else
                {
                    storeId = "";
                }
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("brandId", user.BrandId.ToString()),
                new Claim("storeId", storeId)
            };

            var creds = new SigningCredentials(
                _key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Expires = DateTime.Now.AddDays(7),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
