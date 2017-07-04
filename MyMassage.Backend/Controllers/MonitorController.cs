using MongoDB.Driver;
using Squire.Monitoring.Controllers;
using System;
using System.Threading.Tasks;

namespace MyMassage.Backend.Controllers
{
    public class MonitorController : MonitoringController
    {
        private readonly ISettings settings;

        public MonitorController(ISettings settings) : base()
        {
            this.settings = settings;
        }

        public override async Task<bool> HealthCheck()
        {
            var mc = new MongoClient(settings.Database_Url);
            var db = mc.GetDatabase(settings.Database_Name);
            await db.ListCollectionsAsync();

            return true;
        }
    }
}
