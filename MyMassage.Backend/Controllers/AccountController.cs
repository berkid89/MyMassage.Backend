using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMassage.Backend.Models;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;
using MyMassage.Backend.Resources;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace MyMassage.Backend.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly Security security = new Security();

        public AccountController(ISettings settings) : base(settings) { }

        [HttpPost]
        public IActionResult LogIn([FromBody] User userData)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<User>.Filter.Eq(p => p.Email, userData.Email);
                var user = db.GetCollection<User>("users").Find(filter).FirstOrDefault();

                if (user != null || security.VerifyHash(userData.Password, user.Password))
                    return JResult(getToken(user));
            }

            ModelState.Clear();
            ModelState.AddModelError("InvalidCreds", Common.InvalidCreds);
            return JResult(null);
        }

        private object getToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AppSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(settings.TokenExpirationInMin),
                signingCredentials: creds);

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }
    }
}