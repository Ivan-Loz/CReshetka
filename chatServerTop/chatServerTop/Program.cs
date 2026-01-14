using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace chatServerTop
{
    class Program
    {
        static List<TcpClient> clients = new List<TcpClient>();
        static void Main(string[] args)
        {
            const int port = 5000;

            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Сервер запущен");
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                Console.WriteLine("Клиент тут");

                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);

            }

            
        }
        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            while (true)
            {
                int bytesRead;

                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }
                if (bytesRead == 0)
                {
                    break;
                }
                string mess = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                BroadcastMessage(mess, client);

            }
            clients.Remove(client);
            client.Close();

        }
        static void BroadcastMessage(string mess,TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(mess);

            foreach (TcpClient client in clients)
            {
                if (client != sender)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);

                }
            }
            Console.WriteLine(mess);
        }
    }
}
