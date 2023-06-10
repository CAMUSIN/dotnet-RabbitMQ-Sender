// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;

internal class Program
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using(var connection = factory.CreateConnection())
        {
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable:false, 
                    exclusive:false, autoDelete:false, arguments:null);

                string message = "Hola Mundo";
                var messageBody = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange:"", routingKey:"hello", 
                    basicProperties:null, body:messageBody);

                Console.WriteLine($"[x] Sent { messageBody }");
            }
        }

        System.Console.WriteLine("Press any key to exit...");
        Console.Read();
    }
}