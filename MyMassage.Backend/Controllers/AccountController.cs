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
        private static MD5 md5Hash = MD5.Create();

        public AccountController(ISettings settings) : base(settings) { }

        [HttpPost]
        public IActionResult LogIn([FromBody] User userData)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<User>.Filter.Eq(p => p.Email, userData.Email);
                var user = db.GetCollection<User>("users").Find(filter).FirstOrDefault();

                if (user != null || verifyHash(userData.Password, user.Password))
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

        #region MD5

        private string getHash(string input)
        {
            string hash;

            hash = getMd5Hash(md5Hash, input);

            return hash;
        }

        private bool verifyHash(string input, string hash)
        {
            return verifyMd5Hash(md5Hash, input, hash);
        }

        private string getMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private bool verifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}