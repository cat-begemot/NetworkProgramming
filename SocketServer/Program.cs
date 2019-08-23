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
				Console.Write($"Waiting for connection on the port: ");
				WriteInColor($"{ ipEndPoint}\n", ConsoleColor.DarkYellow);

				Socket socketHandler = socketListener.Accept();

				byte[] receivedMessage = new byte[1024];
				int receivedMessageLength = socketHandler.Receive(receivedMessage);
				var receivedString = Encoding.UTF8.GetString(receivedMessage, 0, receivedMessageLength);

				Console.Write($"Received message from client: ");
				WriteInColor($"{ receivedString}\n\n", ConsoleColor.Blue);

				var repliedString = $"Confirm to receive message. Total {receivedMessageLength} symbol(s).";
				var repliedBytes = Encoding.UTF8.GetBytes(repliedString);
				socketHandler.Send(repliedBytes);

				if (receivedString.IndexOf("<end>") > -1)
				{
					WriteInColor("Server closed the connection.", ConsoleColor.Red);
					break;
				}

				socketHandler.Shutdown(SocketShutdown.Both);
				socketHandler.Close();
			}
        }

		internal static void WriteInColor(string message, ConsoleColor color)
		{
			var previousColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.Write(message);
			Console.ForegroundColor = previousColor;
		}
    }
}
