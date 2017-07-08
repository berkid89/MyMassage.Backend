using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMassage.Backend.Models;
using MongoDB.Driver;

namespace MyMassage.Backend.Controllers
{
    public class GetInTouchController : ControllerBase
    {
        public GetInTouchController(ISettings settings) : base(settings) { }

        [HttpGet]
        public IActionResult Data()
        {
            var data = db.GetCollection<GetInTouch>("getintouch").Find(_ => true).First();

            return JResult(data);
        }

        [HttpPost]
        public IActionResult Data([FromBody] GetInTouch data)
        {
            if (ModelState.IsValid)
            {
                db.GetCollection<GetInTouch>("getintouch").ReplaceOne(_ => true, data);
            }

            return JResult(data);
        }
    }
}