using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Settings
{
    public class MqttSettings
    {
        public const string BrokerId = "test.mosquitto.org";
        public const int BrokerPort = 1883;
        public const string ClientId = "Backend-MQTT-Client";
        public const string Username = "";
        public const string Password = "";
    }
}
