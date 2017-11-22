using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GPDH.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostName = Dns.GetHostName();
            var ipAddresses = Dns.GetHostAddresses(hostName);
            var hostIpAddress = ipAddresses.FirstOrDefault(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork);
            var remoteEndPoint = new IPEndPoint(hostIpAddress, 12345);
            var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(remoteEndPoint);
            Console.WriteLine("Connected to {0}", remoteEndPoint);
            var message = Encoding.Unicode.GetBytes("Xin chào bạn");
            sender.Send(message);
            Console.ReadKey();
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
