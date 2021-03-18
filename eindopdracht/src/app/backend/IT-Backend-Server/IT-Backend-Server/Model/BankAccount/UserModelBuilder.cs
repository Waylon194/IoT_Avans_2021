using IWSN_Backend_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT_Backend_Server.Model.BankAccount
{
  public class UserModelBuilder
  {
    private UserDBModel _Model;

    public UserModelBuilder()
    {
      this._Model = new UserDBModel();
    }

    // Internal user class
    public UserModelBuilder SetName(string name)
    {
      this._Model.Name = name;
      return this;
    }

    // balance available of the user
    public UserModelBuilder SetBalance(int balance)
    {
      this._Model.Balance = balance;
      return this;
    }

    public UserDBModel CreateDBModel()
    {
      return this._Model;
    }
  }
}
