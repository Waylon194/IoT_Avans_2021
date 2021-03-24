using DsmrParser.Dsmr;
using DsmrParser.Models;
using IWSN_Backend_Server.Model.Builders;
using IWSN_Backend_Server.Models.Database;
using IWSN_Backend_Server.Models.Settings.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using IWSN_Backend_Server.Model.Sensor;

namespace IWSN_Backend_Server.Services
{
    /// <summary>
    /// This class handles the Mongo database and MQTT link
    /// -- Gets a MQTT measurement every 20s
    /// </summary>
    public class MongoDBService
    {
        private readonly IMongoCollection<MongoDBDatagramModel> _SensorMeasurementDBCollection;
        private Parser _SmartMeterParser;
        private static MongoDBService _Instance = null; // singleton instance object

        public static MongoDBService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MongoDBService(new IWSNDatabaseSettings());
                }
                return _Instance;
            }
        }

        public MongoDBService(IWSNDatabaseSettings settings)
        {
            // Setting up the connection to the database
            MongoClient mongoDbClient = new MongoClient(settings.DBConnectionString); // connect to the MongoDB via the DB connection string 
            IMongoDatabase databaseData = mongoDbClient.GetDatabase(settings.DatabaseName); // get the IMongoDatabase object via the MongoDB client via a collection

            // Assign the db values to the readonly value
            this._SensorMeasurementDBCollection = databaseData.GetCollection<MongoDBDatagramModel>(settings.DBCollectionName); // get the IMongoCollection object from the IMongoDatabase object

            // create the parser
            this._SmartMeterParser = new Parser();
        }

        public void insertDatagramMeasurement(string json)
        {
            var datagramShell = JsonSerializer.Deserialize<DatagramShell>(json);

            var result = this._SmartMeterParser.Parse(datagramShell.datagram.p1).Result.ToList();
            Console.WriteLine();

            ProcessedDatagram pDatagram = new ProcessedDatagram();
            pDatagram.ParsedTelegrams = result;
            pDatagram.Signature = datagramShell.datagram.signature;
            pDatagram.S0 = datagramShell.datagram.s0;
            pDatagram.S1 = datagramShell.datagram.s1;

            this._SensorMeasurementDBCollection.InsertOneAsync(new MongoDBDatagramModelBuilder()
                .SetMeasurementValue(pDatagram)
                .CreateObject());
        }

        // get all database entries
        public Task<IEnumerable<MongoDBDatagramModel>> GetAllAsync()
        {
            // returns the user if it was found
            return Task.FromResult<IEnumerable<MongoDBDatagramModel>>(this._SensorMeasurementDBCollection.Find(sensor => true).ToList());
        }
    }
}
