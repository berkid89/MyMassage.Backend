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
                db.GetCollection<SpecialOffer>("offers").InsertOne(new SpecialOffer());

                db.GetCollection<Newsletter>("newsletters").InsertOne(new Newsletter());
            }

            return JResult(null);
        }
    }
}
