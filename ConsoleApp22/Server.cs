using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Threading;

namespace TCPServer
{
    public class Server
    {
        public static readonly int Port = 8888;
        public static readonly string Localhost = "127.0.0.1";
        private static TcpListener server;

        public Server()
        {

        }
        public void Start()
        {
            try
            {
                IPAddress localhost = IPAddress.Parse(Localhost);
                server = new TcpListener(localhost, Port);

                server.Start();
                Console.WriteLine("Waiting for connections...");
                
                while(true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Client clientObject = new(client);

                    // создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
}
