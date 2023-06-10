// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;

internal class Program
{
    private static void Main(string[] args)
    {
        //Create Factory - using RabbitMQDockerServer
        var factory = new ConnectionFactory() { HostName = "localhost" };
        //Create Connection
        using(var connection = factory.CreateConnection())
        {
            //Create channel to send/communicate
            using(var channel = connection.CreateModel())
            {
                //Create queue with Exchange if not exist or use it if exist 
                channel.ExchangeDeclare(exchange:"logs", type:ExchangeType.Fanout);

                //Create queue if not exist or use it if exist 
                /*channel.QueueDeclare(queue: "hello", durable:false, 
                    exclusive:false, autoDelete:false, arguments:null);*/

                //Base message
                string message = GetMessage(args);
                //Encode to byte array
                var messageBody = Encoding.UTF8.GetBytes(message);

                //Creating a basic/simple publish 
                /*channel.BasicPublish(exchange:"", routingKey:"hello", 
                    basicProperties:null, body:messageBody);*/

                //Creating a basic publish with exchange
                channel.BasicPublish(exchange:"logs", routingKey:"", basicProperties:null, body:messageBody);
                Console.WriteLine($"[x] Sent { messageBody }");
            }
        }

        System.Console.WriteLine("Press any key to exit...");
        Console.Read();
    }

    private static string GetMessage(string[] args)
    {
        return ((args.Length > 0) 
        ? string.Join(" ", args)
        : "info: Hola Mundo!");
    }
}