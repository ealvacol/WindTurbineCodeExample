using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using SharedModels.Models;

namespace WindturbineMessageProvider.Helpers
{
    public static class RabbitMqHelper
    {
        internal static void SendAggregatedTurbines(SiteOverview turbines)
        {
            var factory = new ConnectionFactory() { 
                HostName = "localhost",
                UserName ="user", 
                Password = SecretManager.GetSecret("RABBIT_MQ_PASSWORD")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "WindParksOverview",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = JsonConvert.SerializeObject(turbines);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "WindParksOverview",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine("Sent {0}", message.ToString());
        }
    }
}
