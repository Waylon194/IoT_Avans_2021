using IWSN_Backend_Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Services
{
    public interface IBankAccountService
    {
        // get all database entries
        public Task<IEnumerable<AccountDBModel>> GetAllAccountsAsync();

        // get a single database entry by id
        public Task<AccountDBModel> GetAccountByIdAsync(string id);

        // create a user 
        public Task<AccountDBModel> CreateAccountAsync(AccountDBModel account);

        // update a existing record/document
        public Task UpdateAccountAsync(string id, AccountDBModel accountUpdated);

        // remove a user-document by a given user referalId
        public Task RemoveAccountAsync(string id);
    }
}