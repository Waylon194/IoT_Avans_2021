using IWSN_Backend_Server.Models.ModelInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Models.Builders
{
    public class UserDBModelBuilder
    {
        private UserDBModel _Model;

        public UserDBModelBuilder()
        {
            this._Model = new UserDBModel();
        }        

        public UserDBModelBuilder SetReferalId(string referalId) 
        { 
            this._Model.ReferalId = referalId;
            return this;
        }

        public UserDBModelBuilder SetUserName(string userName) 
        {
            this._Model.UserName = userName;
            return this;
        }

        public UserDBModelBuilder SetFirstName(string firstName) 
        {
            this._Model.FirstName = firstName;
            return this;
        }

        public UserDBModelBuilder SetSurName(string surName)
        {
            this._Model.SurName = surName;
            return this;
        }

        public UserDBModelBuilder SetLastName(string lastName) 
        {
            this._Model.LastName = lastName;
            return this;
        }
        
        public UserDBModelBuilder SetAge(int age) 
        {
            this._Model.Age = age;
            return this;
        }

        public UserDBModel CreateObject()
        {
            return this._Model;
        }
    }
}
