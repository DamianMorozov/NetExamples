using System;
using System.Net;
using System.Net.Sockets;

namespace ConsoleTcpClient
{
    public class Program
    {
        #region Public and private fields and properties

        internal static int Port { get; set; }
        internal static string Address { get; set; }
        internal static ClientEntity Client { get; set; }

        #endregion

        #region Public and private methods

        internal static void Main()
        {
            Address = "127.0.0.1";
            Port = 8888;
            Console.WriteLine($"Server config: {Address}:{Port}.");
            
            Client = new ClientEntity();
            Console.WriteLine($"Client buffer size: {Client.BufferSize}.");

            TcpListener server = new TcpListener(IPAddress.Parse(Address), Port);
            server.Start();
            Console.WriteLine("Server is started.");
            Console.WriteLine("--------------------------------------------------");

            while (true)
            {
                Client.Work(server.AcceptTcpClient());
                Console.WriteLine($"Client.IsWork: {Client.IsWork}");
                if (!Client.IsWork)
                    break;
            }
        }

        #endregion
    }
}