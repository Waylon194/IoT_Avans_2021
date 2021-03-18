using IWSN_Backend_Server.Model.Builders;
using IWSN_Backend_Server.Model.Settings;
using IWSN_Backend_Server.Model.Settings.Database;
using IWSN_Backend_Server.Models.Database;
using IWSN_Backend_Server.Models.Settings.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Services
{
    public class SensorMeasurementService
    {
        private readonly IMongoCollection<SensorDBModel> _SensorMeasurementDBCollection;

        public SensorMeasurementService(ISensorInfomationDatabaseSettings settings)
        {
            // Setting up the connection to the database
            MongoClient mongoDbClient = new MongoClient(settings.DBConnectionString); // connect to the MongoDB via the DB connection string 
            IMongoDatabase databaseData = mongoDbClient.GetDatabase(settings.DatabaseName); // get the IMongoDatabase object via the MongoDB client via a collection

            // Assign the db values to the readonly value
            this._SensorMeasurementDBCollection = databaseData.GetCollection<SensorDBModel>(settings.DBCollectionName); // get the IMongoCollection object from the IMongoDatabase object

            // check if the collection contains any documents/entries
            if (this._SensorMeasurementDBCollection.CountDocuments(new BsonDocument()) == 0)
            {
                insertDocuments();
            }
        }

        private void insertDocuments()
        {
            this._SensorMeasurementDBCollection.InsertOneAsync(new SensorDBModelBuilder()
                .SetSensorType(SensorTypes.Humidity)
                .SetMeasurementValue(1000)
                .CreateObject());

            this._SensorMeasurementDBCollection.InsertOneAsync(new SensorDBModelBuilder()
                .SetSensorType(SensorTypes.TemperatureSensor)
                .SetMeasurementValue(1410)
                .CreateObject());
        }

        // get all database entries
        public Task<IEnumerable<SensorDBModel>> GetAllAsync()
        {
            // returns the user if it was found
            return Task.FromResult<IEnumerable<SensorDBModel>>(this._SensorMeasurementDBCollection.Find(sensor => true).ToList());
        }

        // get a single database entry by id
        public Task<SensorDBModel> GetByIdAsync(string id)
        {
            // get a user by id
            return Task.FromResult<SensorDBModel>(this._SensorMeasurementDBCollection.Find(sensor => sensor.Id == id).FirstOrDefault());
        }

        // create a user 
        public Task<SensorDBModel> CreateSensorAsync(SensorDBModel sensor)
        {
            // insert a new user entry
            this._SensorMeasurementDBCollection.InsertOneAsync(sensor);
            return Task.FromResult<SensorDBModel>(sensor);
        }

        // update a existing record/document
        public Task UpdateSensorAsync(string id, SensorDBModel sensorToUpdate)
        {
            // find and replace the user with the new user
            return this._SensorMeasurementDBCollection.ReplaceOneAsync(sensor => sensor.Id == id, sensorToUpdate);
        }

        // remove a user-document by a given user referalId
        public Task RemoveSensorAsync(string id)
        {
            return this._SensorMeasurementDBCollection.DeleteOneAsync(sensor => sensor.Id == id);
        }
    }
}
