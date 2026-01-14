using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientTop
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("192.168.0.3", 5000);

            NetworkStream stream = client.GetStream();

            Thread receiveTheread = new Thread(() =>
            {
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string mess = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine(mess);
                }
            });

            receiveTheread.Start();

            Console.WriteLine("Введитк ник: ");
            string name = Console.ReadLine();

            while (true)
            {
                string mess = Console.ReadLine();
                string fullMess = name + ": " + mess;

                byte[] data = Encoding.UTF8.GetBytes(fullMess);

                stream.Write(data, 0, data.Length);

            }
        }
    }
}
