using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GPDH.Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostName = Dns.GetHostName();
            var ipAddresses = Dns.GetHostAddresses(hostName);
            var hostIpAddress = ipAddresses.FirstOrDefault(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork);
            var ipEndPoint = new IPEndPoint(hostIpAddress, 12345);
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipEndPoint);
            server.Listen(10);
            Console.WriteLine("Server Started");
            var count = 0;
            while (true)
            {
                var messageHandler = server.Accept();
                var messageBytes = new byte[1024];
                var numberOfBytes = messageHandler.Receive(messageBytes);
                var message = Encoding.Unicode.GetString(messageBytes, 0, numberOfBytes);
                Console.OutputEncoding = Encoding.Unicode;
                Console.WriteLine(message);
                messageHandler.Shutdown(SocketShutdown.Both);
                messageHandler.Close();
                Console.WriteLine(++count);
            }
            
        }
    }
}
