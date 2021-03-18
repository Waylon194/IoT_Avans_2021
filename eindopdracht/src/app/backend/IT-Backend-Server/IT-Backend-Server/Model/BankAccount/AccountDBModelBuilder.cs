using IWSN_Backend_Server.Models.ModelInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Models.Builders
{
    public class AccountDBModelBuilder
    {
        private AccountDBModel _Model;

        public AccountDBModelBuilder() 
        {
            this._Model = new AccountDBModel();            
        }

        public AccountDBModelBuilder SetBalance(int balance)
        {
            this._Model.Balance = balance;
            return this;
        }

        public AccountDBModelBuilder SetUser(UserDBModel user)
        {
            this._Model.User = user;
            return this;
        }

        public AccountDBModel CopyObject(AccountDBModel toCopy)
        {
            this._Model = toCopy;
            return this._Model;
        }

        public AccountDBModel CreateObject()
        {
            return this._Model;
        }
    }
}
