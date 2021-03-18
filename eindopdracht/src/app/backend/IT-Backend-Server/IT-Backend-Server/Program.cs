using IWSN_Backend_Server.Mqtt;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWSN_Backend_Server
{
    public class Program
    {
        static IManagedMqttClient client;

        public static async Task Main(string[] args)
        {

            await MqttConnectAsync();
            await SubscribeAsync("VdR-Test");
            
            // Run REST server API configurations
            CreateHostBuilder(args).Build().Run();
        }

        // Credit to => https://dzone.com/articles/mqtt-publishing-and-subscribing-messages-to-mqtt-b
        public static async Task MqttConnectAsync()
        {
            string clientId = Guid.NewGuid().ToString(); // create a random ID
            string mqttURI = "test.mosquitto.org";
            string mqttUser = "";
            string mqttPassword = "";
            int mqttPort = 1883;
            bool mqttSecure = false;

              var messageBuilder = new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithCredentials(mqttUser, mqttPassword)
                .WithTcpServer(mqttURI, mqttPort)
                .WithCleanSession();

            var options = mqttSecure
              ? messageBuilder
                .WithTls()
                .Build()
              : messageBuilder
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
              .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
              .WithClientOptions(options)
              .Build();

            client = new MqttFactory().CreateManagedMqttClient();

            client.UseApplicationMessageReceivedHandler(e => 
            {
                try
                {
                    string topic = e.ApplicationMessage.Topic;

                    if (string.IsNullOrWhiteSpace(topic) == false)
                    {
                        string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        Console.WriteLine($"Topic: {topic}. Message Received: {payload}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex);
                }
            });
            await client.StartAsync(managedOptions);
        }

        public static async Task SubscribeAsync(string topic, int qos = 1)
        {
            await client.SubscribeAsync(new MqttTopicFilterBuilder()
            .WithTopic(topic)
            .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
            .Build());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
