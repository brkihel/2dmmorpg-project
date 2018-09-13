using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bindings;

namespace _2smmorpg_server
{
    class General
    {
        private ServerTCP stcp;

        public void InitializeServer()
        {
            stcp = new ServerTCP();

            for(int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                ServerTCP.Clients[i] = new Client();
            }

            stcp.InitializeNetwork();
            Console.WriteLine("O servidor foi iniciado com sucesso!");
        }
    }
}
