using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace messeg
{
    class Program
    {


        static void Main(string[] args)
        {
            const int port = 5000;

            TcpListener server = new TcpListener(IPAddress.Any, port);

            server.Start();
            Console.WriteLine("Сервер запущен");

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Клиент тут");

            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string mess = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine("Сообщение: "+ mess);

            string response = "Сообщение получино сервером";
            byte[] responseData = Encoding.UTF8.GetBytes(response);

            stream.Write(responseData, 0, responseData.Length);

            client.Close();
            server.Stop();
            Console.ReadKey();

        }
    }
}
