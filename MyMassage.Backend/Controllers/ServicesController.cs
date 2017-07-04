using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MyMassage.Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyMassage.Backend.Controllers
{
    public class ServicesController : ControllerBase
    {
        public ServicesController(ISettings settings) : base(settings) { }

        [HttpGet]
        public IActionResult List()
        {
            var services = db.GetCollection<Service>("services");
            return JResult(services);
        }

        [HttpPut]
        public IActionResult Add([FromBody] Service service)
        {
            if (ModelState.IsValid)
            {
                db.GetCollection<Service>("services").InsertOne(service);
            }

            return JResult(service);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var filter = Builders<Service>.Filter.Eq(p => p.Id, ObjectId.Parse(id));
            var service = db.GetCollection<Service>("services").Find(filter).First();
            return JResult(service);
        }

        [HttpPost]
        public IActionResult Edit(string id, [FromBody] Service service)
        {
            if (ModelState.IsValid)
            {
                service.Id = ObjectId.Parse(id);
                var filter = Builders<Service>.Filter.Eq(p => p.Id, service.Id);
                db.GetCollection<Service>("services").ReplaceOne(filter, service);
            }

            return JResult(service);
        }
    }
}
