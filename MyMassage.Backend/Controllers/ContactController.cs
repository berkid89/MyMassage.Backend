using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MyMassage.Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyMassage.Backend.Controllers
{
    public class ContactController : ControllerBase
    {
        public ContactController(ISettings settings) : base(settings) { }

        [HttpPost]
        public IActionResult Send([FromBody] Contact contact)
        {
            if (ModelState.IsValid)
            {
                //TODO: Send email
            }

            return JResult(contact);
        }
    }
}
