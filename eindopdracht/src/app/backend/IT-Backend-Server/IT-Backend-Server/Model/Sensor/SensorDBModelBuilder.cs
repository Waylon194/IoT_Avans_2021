using IWSN_Backend_Server.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Model.Builders
{
    public class SensorDBModelBuilder
    {
        private SensorDBModel _Model;
        
        public SensorDBModelBuilder()
        {
            this._Model = new SensorDBModel();
            this._Model.DateOfMeasurement = DateTime.Now; // set the time on creating a new Model
        }

        public SensorDBModelBuilder SetSensorType(SensorTypes sensorType)
        {
            this._Model.SensorType = sensorType;
            return this;
        }

        public SensorDBModelBuilder SetMeasurementValue(int value)
        {
            this._Model.Value = value;
            return this;
        }

        public SensorDBModel CreateObject()
        {
            return this._Model;
        }
    }
}
