using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TCPServer;

namespace TCPClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            TcpClient client = new();

            try
            {
                client = new TcpClient(Server.Localhost, Server.Port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    // sending
                    Console.Write(username + ": ");
                    string message = Console.ReadLine();
                    message = $"{username}: {message}";
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    // receiving
                    data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    Console.WriteLine($"Сервер: {message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
