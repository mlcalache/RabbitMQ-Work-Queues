using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_Work_Queues.Data.SqlServer.EF.Repositories;
using RabbitMQ_Work_Queues.Domain.Entities;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ_Work_Queues.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("SERVER");
            Console.ForegroundColor = ConsoleColor.White;

            //ReceiveMessage();

            //ReceiveMultipleMessages();

            ReceiveMultipleMessages2();
        }

        private static void ReceiveMultipleMessages2()
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

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    Console.WriteLine(" [*] Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($" [x] Received {message} on {DateTime.Now}");

                        var count = 0;

                        count = InsertIntoSql(message);

                        if (count > 0)
                        {
                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                            Console.WriteLine($" [x] Done on {DateTime.Now}");
                        }
                    };

                    channel.BasicConsume(queue: "task_queue",
                                         autoAck: false,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        private static int InsertIntoSql(string message)
        {
            int count;

            using (var repository = new Log_Portal_GenericRepository())
            {
                repository.ChangeConnection("testwolford");

                var entity = new Log_Portal_Generic
                {
                    ClientIP = "localhost",
                    Message = message,
                    RegistrationDateTime = DateTime.Now,
                    Sender = "Queue",
                    ThreadId = Guid.NewGuid(),
                    UserId = 2600,
                    Type = 1
                };

                repository.Create(entity);

                count = repository.SaveChanges();
            }

            return count;
        }

        private static void ReceiveMultipleMessages()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "task_queue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    Console.WriteLine(" [*] Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($" [x] Received {message}");

                        var dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [x] Done");

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume(queue: "task_queue",
                                         autoAck: false,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        private static void ReceiveMessage()
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

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($@" [x] Received {message}");
                    };

                    channel.BasicConsume(queue: "my-jobs",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}