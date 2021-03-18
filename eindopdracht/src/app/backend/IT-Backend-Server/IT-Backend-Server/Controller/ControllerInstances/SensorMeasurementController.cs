using IWSN_Backend_Server.Models.Database;
using IWSN_Backend_Server.Models.Settings.Class;
using IWSN_Backend_Server.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Controllers.ControllerInstances
{
    [Route( DBRouteSettings.SensorMainRouteName )]
    [ApiController]
    public class SensorMeasurementController : ControllerBase
    {
        private readonly SensorMeasurementService _SensorMeasurementService;

        public SensorMeasurementController(SensorMeasurementService service)
        {
            // assign the service to the class variable
            this._SensorMeasurementService = service;
        }

        // get all the available users - async
        [Route( DBRouteSettings.SensorSubRouteName + "/all" )]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorDBModel>>> GetAllUsers()
        {
            var users = await this._SensorMeasurementService.GetAllAsync();
            return users.ToList();
        }

        // get a single user by MongoDB assign Id
        [Route( DBRouteSettings.SensorSubRouteName + "/{id}" )]
        [HttpGet]
        public async Task<ActionResult<SensorDBModel>> GetById(string id)
        {
            var user = await this._SensorMeasurementService.GetByIdAsync(id);
            return user;
        }

        // Create a single user document in MongoDB - works but throws an error? -> System.InvalidOperationException: No route matches the supplied values.
        [Route( DBRouteSettings.SensorSubRouteName + "/create" )]
        [HttpPost]
        public async Task<ActionResult<SensorDBModel>> CreateUser([FromBody] SensorDBModel user)
        {
            var userCreated = await _SensorMeasurementService.CreateSensorAsync(user);
            return userCreated;
        }

        // 
        [Route( DBRouteSettings.SensorSubRouteName + "/update/{id}" )]
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] SensorDBModel user)
        {
            var gatheredUser = await this._SensorMeasurementService.GetByIdAsync(id);

            if (gatheredUser == null)
            {
                return NotFound();
            }
            await this._SensorMeasurementService.UpdateSensorAsync(id, user);
            return NoContent();
        }

        //
        [Route( DBRouteSettings.SensorSubRouteName + "/delete/{id}" )]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var accountTaskObject = await this._SensorMeasurementService.GetByIdAsync(id);

            if (accountTaskObject == null)
            {
                return NotFound();
            }
            await this._SensorMeasurementService.RemoveSensorAsync(id);

            return NoContent();
        }
    }
}
