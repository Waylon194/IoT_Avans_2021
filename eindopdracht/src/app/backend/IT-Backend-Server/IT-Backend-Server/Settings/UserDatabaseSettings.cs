using IWSN_Backend_Server.Model.Settings;
using IWSN_Backend_Server.Model.Settings.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Models
{
  public class UserDatabaseSettings : IUserDatabaseSettings
  {
    public string DBCollectionName { get; set; }
    public string DBConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }
}
