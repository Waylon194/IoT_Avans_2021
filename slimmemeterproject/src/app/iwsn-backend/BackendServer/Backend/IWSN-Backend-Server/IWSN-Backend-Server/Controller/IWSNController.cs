using IWSN_Backend_Server.Model.Sensor;
using IWSN_Backend_Server.Models.Database;
using IWSN_Backend_Server.Models.Settings.Class;
using IWSN_Backend_Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Controllers.ControllerInstances
{
    /// <summary>
    /// Controller class of the REST API (via HTTP communication)
    /// </summary>
    [Route( IWSNControllerRouteSettings.IWSNMainRouteName )]
    [ApiController]
    public class IWSNController : ControllerBase
    {
        private readonly MongoDBService _SensorMeasurementService;
        private readonly int MAX_LATEST_RANGE_ALLOWED = 20;

        public IWSNController()
        {
            // Assign the service to the class variable
            this._SensorMeasurementService = MongoDBService.Instance;
        }        

        // ROUTE: .../iwsn/all
        // get all the available users - async
        [Route( "/all" )]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MongoDBDatagramModel>>> GetAllMeasurements()
        {
            var measurements = await this._SensorMeasurementService.GetAllAsync();
            return measurements.ToList();
        }

        // ROUTE: .../iwsn/latest/all
        // get lastest measurements available bases on variable => LATEST_RANGE_ALLOWED defined as private attribute
        [Route("/latest/all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MongoDBDatagramModel>>> GetLatestMeasurements()
        {
            var measurements = await this._SensorMeasurementService.GetAllAsync();
            var count = measurements.Count();

            if (count <= MAX_LATEST_RANGE_ALLOWED)
            {
                return measurements.ToList();
            }
            return measurements.ToList().GetRange(count - MAX_LATEST_RANGE_ALLOWED, MAX_LATEST_RANGE_ALLOWED);
        }

        // ROUTE: .../iwsn/latest/solar
        // get all the available users - async
        [Route( "/lastest/solar")]
        [HttpGet]
        public async Task<ActionResult<SObject>> GetLatestSolarMeasurement()
        {
            var measurements = await this._SensorMeasurementService.GetAllAsync();
            return measurements.ToList().Last().Datagram.SolarPanel;
        }

        // 
    }
}
