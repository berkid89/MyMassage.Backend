using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMassage.Backend.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly ISettings settings;

        protected readonly IMongoDatabase db;

        public ControllerBase(ISettings settings) : base()
        {
            this.settings = settings;

            var mc = new MongoClient(settings.Database_Url);
            db = mc.GetDatabase(settings.Database_Name);
        }

        protected JsonResult JResult(object data)
        {
            return new JsonResult(
                new
                {
                    errors = ModelState.IsValid ? null : ModelState.Select(p => new { key = p.Key, propErrors = p.Value.Errors.Select(e => e.ErrorMessage) }),
                    data = data
                },
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
