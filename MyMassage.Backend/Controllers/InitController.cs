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
        public InitController(ISettings settings) : base(settings) { }

        [HttpPost]
        public IActionResult Db(string secret)
        {
            if (Guid.Parse(secret) == Guid.Parse("7c9e6679-1234-40dc-946b-e07fc1f90ae7"))
            {
                var users = db.GetCollection<User>("users");
                users.InsertMany(new List<User>
                {
                    new User()
                    {
                        Email = "berkid89@gmail.com",
                        Password = "e90596e472a0eac0199181a8713adb92"
                    },
                    new User()
                    {
                        Email = "ledybag@hotmail.hu",
                        Password = "00cb18d25cb6ffb5dc3489dfb4ee4dd6"
                    },
                });

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
    }
}
