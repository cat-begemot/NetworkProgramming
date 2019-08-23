using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
			try
			{
				SendMessageToServer(11000);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				Console.ReadLine();
			}
        }

		internal static void SendMessageToServer(int port)
		{
			var receivedServerResponse = new byte[1024];

			var ipHostEntry = Dns.GetHostEntry("localhost");
			var ipAddress = ipHostEntry.AddressList[0];
			var ipEndPoint = new IPEndPoint(ipAddress, port);

			var socketSender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

			socketSender.Connect(ipEndPoint);

			Console.Write("Enter the message: ");
			var previousColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Blue;
			var clientMessage = Console.ReadLine();
			Console.ForegroundColor = previousColor;

			Console.Write("Connecting with ");
			WriteInColor($"{socketSender.RemoteEndPoint}", ConsoleColor.DarkYellow);
			Console.WriteLine("...");

			var clientMessageInBytes = Encoding.UTF8.GetBytes(clientMessage);
			int sentClientMessageLength = socketSender.Send(clientMessageInBytes);
			int receivedServerResponseLength = socketSender.Receive(receivedServerResponse);

			Console.Write($"Server responded: ");
			WriteInColor($"{Encoding.UTF8.GetString(receivedServerResponse, 0, receivedServerResponseLength)}\n\n",
				ConsoleColor.Red);

			if (clientMessage.IndexOf("<end>") == -1)
				SendMessageToServer(port);

			socketSender.Shutdown(SocketShutdown.Both);
			socketSender.Close();
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
