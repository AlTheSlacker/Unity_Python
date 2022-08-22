using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace PythonComms
{
	public class PythonComms : MonoBehaviour
	{
        void Start()
		{
            // Start TcpServer background thread
            Thread tcpListenerThread;
            tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
			tcpListenerThread.IsBackground = true;
			tcpListenerThread.Start();
		}


		private void ListenForIncommingRequests()
		{
			TcpListener tcpListener;
			TcpClient connectedTcpClient;

			try
			{
				// Create listener on localhost port 8052. 			
				tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
				tcpListener.Start();
				Debug.Log("Server is listening");
				Byte[] bytes = new Byte[1024];
				while (true)
				{
					using (connectedTcpClient = tcpListener.AcceptTcpClient())
					{
                        // Get a stream object for reading 					
                        using NetworkStream stream = connectedTcpClient.GetStream();
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.Log("client message received as: " + clientMessage);
                        }
                    }
				}
			}
			catch (SocketException socketException)
			{
				Debug.Log("SocketException " + socketException.ToString());
			}
		}

	}
}