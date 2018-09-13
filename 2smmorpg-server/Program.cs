using System.Threading;
using System;

namespace _2smmorpg_server
{
    class Program
    {
        private static Thread consoleThread;
        private static General general;

        static void Main(string[] args)
        {
            general = new General();
            consoleThread = new Thread(new ThreadStart(ConsoleThread));
            consoleThread.Start();
            general.InitializeServer();
        }

        static void ConsoleThread()
        {
            Console.ReadLine();
        }
    }
}
