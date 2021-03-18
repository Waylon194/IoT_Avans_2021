using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Models.Settings.Class
{
    public class DBRouteSettings
    {
        public const string MainRoute = "api/v1";

        public const string UsersMainRouteName = MainRoute + "/banking/"; 
        public const string UsersSubRouteName = "user";
    }
}
