using Squire.Monitoring.Controllers;
using System;
using System.Threading.Tasks;

namespace MyMassage.Backend.Controllers
{
    public class MonitorController : MonitoringController
    {
        public override Task<bool> HealthCheck()
        {
            throw new NotImplementedException();
        }
    }
}
