using RabbitMQ.Client;
using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("CLIENT");
            Console.ForegroundColor = ConsoleColor.White;

            //SendSingleMessageToBroker();

            //SendSingleMessageToBrokerWithSleep(args);

            //var quantity = 10;

            var quantity = Convert.ToInt32(args[0]);

            SendMultipleMessagesToBroker(quantity);
        }

        private static void SendMultipleMessagesToBroker(int quantity)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "task_queue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    for (int i = 1; i <= quantity; i++)
                    {
                        var message = $"message {i}...";

                        var body = Encoding.UTF8.GetBytes(message);

                        var properties = channel.CreateBasicProperties();

                        properties.Persistent = true;

                        channel.BasicPublish(exchange: "",
                                             routingKey: "task_queue",
                                             basicProperties: properties,
                                             body: body);

                        Console.WriteLine($" [x] Sent {message}");
                    }
                }
            }

            Console.WriteLine(" Press [enter] to exit.");

            Console.ReadLine();
        }

        private static void SendSingleMessageToBrokerWithSleep(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "task_queue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var message = GetMessage(args);

                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();

                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: "task_queue",
                                         basicProperties: properties,
                                         body: body);

                    Console.WriteLine($" [x] Sent {message}");
                }
            }

            Console.WriteLine(" Press [enter] to exit.");

            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }

        private static void SendSingleMessageToBroker()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "my-jobs",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var message = "Hello World!";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "my-jobs",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($@" [x] Sent {message}");
                }

                Console.WriteLine(" Press [enter] to exit.");

                Console.ReadLine();
            }
        }
    }
}