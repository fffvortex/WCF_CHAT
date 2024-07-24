using System;
using System.ServiceModel;

namespace chat_host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(wcf_chat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Host is started");
                Console.ReadLine();
            }
        }
    }
}
