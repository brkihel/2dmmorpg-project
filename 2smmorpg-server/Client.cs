using System;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace _2smmorpg_server
{
    class Client
    {
        public int Index;
        public string IP;
        public TcpClient Socket;
        public NetworkStream myStream;
        public bool Closing;
        public byte[] readBuff;

        public void Start()
        {
            Socket.SendBufferSize = 4096;
            Socket.ReceiveBufferSize = 4096;
            myStream = Socket.GetStream();
            Array.Resize(ref readBuff, Socket.ReceiveBufferSize);
            myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceiveData, null);
        }

        private void OnReceiveData(IAsyncResult ar)
        {
            try
            {
                int readBytes = myStream.EndRead(ar);
                if (readBytes <= 0)
                {
                    CloseSocket(Index); //Desconecta o player se houver erro no recebimento de packets(receber 0 ou menos)
                        return;
                }

                byte[] newbytes = null;
                Array.Resize(ref newbytes, readBytes);
                Buffer.BlockCopy(readBuff, 0, newbytes, 0, readBytes);
                //Lê todos os pacotes enviados para que possamos manipular esses dados futuramente.

                myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceiveData, null);
            }

            catch
            {
                CloseSocket(Index);
            }
        }

        private void CloseSocket(int index)
        {
            Console.WriteLine("A conxeão com " + IP + "foi interrompida.");
            Socket.Close();
            Socket = null;

        }
    }
}
