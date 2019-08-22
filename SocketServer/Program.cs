using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
			var ipHostEntry = Dns.GetHostEntry("localhost");
			IPAddress ipAddress = ipHostEntry.AddressList[0];
			var ipEndPoint = new IPEndPoint(ipAddress, 11000);

			var socketListener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			socketListener.Bind(ipEndPoint);
			socketListener.Listen(10);

			while (true)
			{
				Console.WriteLine($"Waiting for connection from client on the port: {ipEndPoint}");

				Socket socketHandler = socketListener.Accept();

				byte[] receivedMessage = new byte[1024];
				int receivedMessageLength = socketHandler.Receive(receivedMessage);
				var receivedString = Encoding.UTF8.GetString(receivedMessage, 0, receivedMessageLength);

				Console.WriteLine($"Received message from client: {receivedString}");

				var repliedString = $"Confirm to receive message. Total {receivedMessageLength} symbol(s).";
				var repliedBytes = Encoding.UTF8.GetBytes(repliedString);
				socketHandler.Send(repliedBytes);

				if (receivedString.IndexOf("<end>") > -1)
				{
					Console.WriteLine("Server closed the connection.");
					break;
				}

				socketHandler.Shutdown(SocketShutdown.Both);
				socketHandler.Close();
			}
        }
    }
}
