using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class SendMessage
    {
        public static void Main(string[] args)
        {           
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Name",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string _name = GetName(args);            
                       
                    var body = Encoding.UTF8.GetBytes(_name); 
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Name",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine("Sent: {0}", _name);
                }

                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
            }
        }

        private static string GetName(string[] args)
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine("Please enter your name:");

            UserName p = new UserName(Console.ReadLine());
            string _user = p.NameMethod();

            return ((args.Length > 0) ? string.Join("Hello my name is, ", args) : _user);
        }
    }

    class UserName
    {
        string userName;
        public string FirstName { get { return userName; } set { userName = value; } }

        public UserName(string _name)
        {
            userName = _name;
        }

        public string NameMethod()
        {
            string x = userName;
            return x;
        }
    }
}
