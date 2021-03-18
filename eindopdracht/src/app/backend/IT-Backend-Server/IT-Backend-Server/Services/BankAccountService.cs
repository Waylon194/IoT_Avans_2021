using IT_Backend_Server.Model.BankAccount;
using IWSN_Backend_Server.Model.Settings.Database;
using IWSN_Backend_Server.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Services
{
  /// <summary>
  /// Class which is used as a service to connect to the BankUser MongoDatabase
  /// It uses Async Tasking to wait for results and support the use of Threading
  /// </summary>
  public class BankAccountService : IBankAccountService
  {
    // generic database values
    private readonly IMongoCollection<UserDBModel> _UsersDBCollection;

    public BankAccountService(IUserDatabaseSettings settings)
    {
      // Setting up the connection to the database
      MongoClient mongoDbClient = new MongoClient(settings.DBConnectionString); // connect to the MongoDB via the DB connection string 
      IMongoDatabase databaseData = mongoDbClient.GetDatabase(settings.DatabaseName); // get the IMongoDatabase object via the MongoDB client via a collection

      // Assign the db values to the readonly value
      this._UsersDBCollection = databaseData.GetCollection<UserDBModel>(settings.DBCollectionName); // get the IMongoCollection object from the IMongoDatabase object

      if (this._UsersDBCollection.CountDocuments(new BsonDocument()) == 0)
      {
        insertDocuments();
      }
    }

    private void insertDocuments()
    {
      this._UsersDBCollection.InsertOneAsync(new UserModelBuilder()
        .SetName("Diedrich")
        .SetBalance(1000)
        .CreateDBModel());

      this._UsersDBCollection.InsertOneAsync(new UserModelBuilder()
         .SetName("Edwin")
         .SetBalance(1000)
         .CreateDBModel());

      this._UsersDBCollection.InsertOneAsync(new UserModelBuilder()
         .SetName("Wilco")
         .SetBalance(1000)
         .CreateDBModel());
    }

    // get all database entries
    public Task<IEnumerable<UserDBModel>> GetAllAccountsAsync()
    {
      // returns the user if it was found
      return Task.FromResult<IEnumerable<UserDBModel>>(this._UsersDBCollection.Find(user => true).ToList());
    }

    // get a single database entry by id
    public Task<UserDBModel> GetAccountByNameAsync(string name)
    {
      // get a user by id
      return Task.FromResult<UserDBModel>(this._UsersDBCollection.Find(user => user.Name == name).FirstOrDefault());
    }

    // create a user 
    public Task<UserDBModel> CreateAccountAsync(UserDBModel account)
    {
      // insert a new user entry
      this._UsersDBCollection.InsertOneAsync(account);
      return Task.FromResult<UserDBModel>(account);
    }

    // update a existing record/document
    public Task UpdateAccountAsync(string name, UserDBModel accountUpdated)
    {
      // find and replace the user with the new user
      var filter = Builders<UserDBModel>.Filter.Eq("Name", name);
      var update = Builders<UserDBModel>.Update.Set("Balance", accountUpdated.Balance);

      return this._UsersDBCollection.UpdateOneAsync(filter, update);

    }

    // remove a user-document by a given user referalId
    public Task RemoveAccountAsync(string name)
    {
      return this._UsersDBCollection.DeleteOneAsync(account => account.Name == name);
    }
  }
}
