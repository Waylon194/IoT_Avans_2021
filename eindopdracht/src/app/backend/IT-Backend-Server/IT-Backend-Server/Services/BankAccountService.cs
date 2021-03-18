using IWSN_Backend_Server.Model.Settings;
using IWSN_Backend_Server.Model.Settings.Database;
using IWSN_Backend_Server.Models;
using IWSN_Backend_Server.Models.Builders;
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
        private readonly IMongoCollection<AccountDBModel> _AccountsDBCollection;

        public BankAccountService(IBankAccountDatabaseSettings settings)
        {
            // Setting up the connection to the database
            MongoClient mongoDbClient = new MongoClient(settings.DBConnectionString); // connect to the MongoDB via the DB connection string 
            IMongoDatabase databaseData = mongoDbClient.GetDatabase(settings.DatabaseName); // get the IMongoDatabase object via the MongoDB client via a collection

            // Assign the db values to the readonly value
            this._AccountsDBCollection = databaseData.GetCollection<AccountDBModel>(settings.DBCollectionName); // get the IMongoCollection object from the IMongoDatabase object

            if (this._AccountsDBCollection.CountDocuments(new BsonDocument()) == 0)
            {
                insertDocuments();
            }
        }

        private void insertDocuments()
        {
            this._AccountsDBCollection.InsertOneAsync(new AccountDBModelBuilder()
                .SetBalance(1000)
                .SetUser(new UserDBModelBuilder()
                    .SetReferalId("JennLown")
                    .SetUserName("Jennny_21")
                    .SetFirstName("Jennifer")
                    .SetSurName("")
                    .SetLastName("Lown")
                    .SetAge(21)
                    .CreateObject()
                )
                .CreateObject());

            this._AccountsDBCollection.InsertOneAsync(new AccountDBModelBuilder()
                .SetBalance(1000)
                .SetUser(new UserDBModelBuilder()
                    .SetReferalId("JasonLown")
                    .SetUserName("Jay_23")
                    .SetFirstName("Jason")
                    .SetSurName("")
                    .SetLastName("Lown")
                    .SetAge(23)
                    .CreateObject()
                )
                .CreateObject());
        }

        // get all database entries
        public Task<IEnumerable<AccountDBModel>> GetAllAccountsAsync()
        {
            // returns the user if it was found
            return Task.FromResult<IEnumerable<AccountDBModel>>(this._AccountsDBCollection.Find(user => true).ToList());
        }

        // get a single database entry by id
        public Task<AccountDBModel> GetAccountByIdAsync(string id)
        {
            // get a user by id
            return Task.FromResult<AccountDBModel>(this._AccountsDBCollection.Find(user => user.Id == id).FirstOrDefault());
        }

        // create a user 
        public Task<AccountDBModel> CreateAccountAsync(AccountDBModel account)
        {
            // insert a new user entry
            this._AccountsDBCollection.InsertOneAsync(account);
            return Task.FromResult<AccountDBModel>(account);
        }

        // update a existing record/document
        public Task UpdateAccountAsync(string id, AccountDBModel accountUpdated)
        {
            // find and replace the user with the new user
            return this._AccountsDBCollection.ReplaceOneAsync(account => account.Id == id, accountUpdated);
        }

        // remove a user-document by a given user referalId
        public Task RemoveAccountAsync(string id)
        {
            return this._AccountsDBCollection.DeleteOneAsync(account => account.Id == id);
        }
    }
}
