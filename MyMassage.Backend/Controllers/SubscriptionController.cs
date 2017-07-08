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
                var collection = db.GetCollection<Subscription>("subscriptions");
                var filter = Builders<Subscription>.Filter.Eq(p => p.Email, subscription.Email);
                var same = collection.Find(filter).FirstOrDefault();

                if (same == null)
                    collection.InsertOne(subscription);
            }

            return JResult(subscription);
        }

        [HttpDelete]
        public IActionResult UnSubscribe(string id)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<Subscription>.Filter.Eq(p => p.Id, id);
                db.GetCollection<Subscription>("subscriptions").DeleteOne(filter);
            }

            return JResult(null);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var newsletter = db.GetCollection<Newsletter>("newsletters").Find(_ => true).First();

            return JResult(newsletter);
        }

        [HttpPost]
        public IActionResult Edit([FromBody] Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                db.GetCollection<Newsletter>("newsletters").ReplaceOne(_ => true, newsletter);
            }

            return JResult(newsletter);
        }
    }
}
