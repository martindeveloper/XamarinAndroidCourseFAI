using System;
using System.Net.Sockets;
using System.Text;

namespace Products.Model
{
	public class SocketModel
	{
		private readonly Socket MainSocket;

		public SocketModel ()
		{
			MainSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		public void Connect()
		{
			MainSocket.Connect ("10.5.142.138", 8090);
		}

		public void SendPayload(string payload)
		{
			byte[] bytes = Encoding.ASCII.GetBytes (payload);

			MainSocket.Send (bytes);

			byte[] responseBytes = new byte[4096];

			int bytesRecived = MainSocket.Receive (responseBytes);

			MainSocket.Shutdown (SocketShutdown.Both);
			MainSocket.Close ();

			string response = Encoding.ASCII.GetString (responseBytes);

			Console.WriteLine (response);
		}
	}
}

