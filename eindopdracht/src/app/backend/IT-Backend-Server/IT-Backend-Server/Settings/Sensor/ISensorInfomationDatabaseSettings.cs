using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Model.Settings.Database
{
    public interface ISensorInfomationDatabaseSettings
    {
        string DBCollectionName { get; set; }
        string DBConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
