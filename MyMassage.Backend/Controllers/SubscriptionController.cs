using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MyMassage.Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyMassage.Backend.Controllers
{
    public class SubscriptionController : ControllerBase
    {
        public SubscriptionController(ISettings settings) : base(settings) { }

        [HttpPut]
        public IActionResult Subscribe([FromBody] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.GetCollection<Subscription>("subscriptions").InsertOne(subscription);
            }

            return JResult(subscription);
        }

        [HttpDelete]
        public IActionResult UnSubscribe(string id)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<Subscription>.Filter.Eq(p => p.Id, ObjectId.Parse(id));
                db.GetCollection<Subscription>("subscriptions").DeleteOne(filter);
            }

            return JResult("OK");
        }
    }
}
