using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMassage.Backend.Models;
using MongoDB.Driver;

namespace MyMassage.Backend.Controllers
{
    public class OfferController : ControllerBase
    {
        public OfferController(ISettings settings) : base(settings) { }

        [HttpGet]
        public IActionResult Special()
        {
            var specialOffer = db.GetCollection<SpecialOffer>("offers").Find(_ => true).First();

            return JResult(specialOffer);
        }

        [HttpPost]
        public IActionResult Special([FromBody] SpecialOffer specialOffer)
        {
            if (ModelState.IsValid)
            {
                db.GetCollection<SpecialOffer>("offers").ReplaceOne(_ => true, specialOffer);
            }

            return JResult(specialOffer);
        }
    }
}
