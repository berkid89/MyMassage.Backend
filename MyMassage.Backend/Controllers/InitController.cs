using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMassage.Backend.Models;

namespace MyMassage.Backend.Controllers
{
    public class InitController : ControllerBase
    {
        private readonly Security security = new Security();

        public InitController(ISettings settings) : base(settings) { }

        [HttpPost]
        public IActionResult Db(string secret)
        {
            if (secret == settings.AppSecret)
            {
                db.GetCollection<SpecialOffer>("offers").InsertOne(new SpecialOffer());

                db.GetCollection<Newsletter>("newsletters").InsertOne(new Newsletter());

                db.GetCollection<GetInTouch>("getintouch").InsertOne(new GetInTouch()
                {
                    Address = "Károly krt. 3/a. Fél emelet 13. (33-as kapucsengő)",
                    City = "Budapest 1075",
                    Country = "Magyarország",
                    PhoneNumber = "+36/30-916-1147",
                    Email = "masszazsterapia.eletmod@gmail.com"
                });
            }

            return JResult(null);
        }

        [HttpPost]
        public IActionResult Account(string secret, string email, string password)
        {
            if (secret == settings.AppSecret)
            {
                var users = db.GetCollection<User>("users");
                users.InsertOne(new User()
                {
                    Email = email,
                    Password = security.GetHash(password),
                });
            }

            return JResult(null);
        }
    }
}
