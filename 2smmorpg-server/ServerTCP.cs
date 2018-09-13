using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Bindings;

namespace _2smmorpg_server
{
    class ServerTCP
    {
        public static Client[] Clients = new Client[Constants.MAX_PLAYERS];
        public TcpListener ServerSocket;

        public void InitializeNetwork()
        {
            Console.WriteLine("Inicializando a conexão com o servidor...");

            ServerSocket = new TcpListener(IPAddress.Any, 5555);
            ServerSocket.Start();
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);
        }

        private void OnClientConnect(IAsyncResult ar)
        {
            TcpClient client = ServerSocket.EndAcceptTcpClient(ar);
            client.NoDelay = false;
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);

            for (int i = 1; i <= Constants.MAX_PLAYERS; i++)
            {
                if(Clients[i].Socket == null)
                {
                    Clients[i].Socket = client;
                    Clients[i].Index = i;
                    Clients[i].IP = client.Client.RemoteEndPoint.ToString();
                    Clients[i].Start();
                    Console.WriteLine("Conexão bem sucedida com " + Clients[i].IP);
                    return;
                }
            }
        }
    }
}
