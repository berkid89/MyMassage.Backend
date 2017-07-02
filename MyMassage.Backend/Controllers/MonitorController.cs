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
            var mc = new MongoClient(settings.DatabaseUrl);
            var db = mc.GetDatabase(settings.DatabaseName);
            await db.ListCollectionsAsync();

            return true;
        }
    }
}
