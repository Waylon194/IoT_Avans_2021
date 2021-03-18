using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Models.Database
{
    public class SensorDBModel
    {
        /// <summary>
        /// <- Remark -> - When using the System.Text.Json make sure the attributes are reachable with { get; set; }!
        /// </summary>
        public SensorDBModel() { }

        // This is used for marking the MongoDB assigned unique id
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SensorType")]
        public SensorTypes SensorType { get; set; }

        [BsonElement("MeasurementDate")]
        public DateTime DateOfMeasurement { get; set; }

        [BsonElement("MeasurementValue")]
        public int Value { get; set; }
    }

    public enum SensorTypes
    {
        TemperatureSensor,
        Humidity
    }
}
