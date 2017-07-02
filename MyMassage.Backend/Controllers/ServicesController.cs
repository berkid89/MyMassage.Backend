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
    }
}
