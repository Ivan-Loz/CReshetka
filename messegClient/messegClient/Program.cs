using System;
using System.Text;
using System.Net.Sockets;

namespace messegClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("192.168.0.3", 5000);

            NetworkStream stream = client.GetStream();
            Console.WriteLine("Введите сообщение: ");
            string mess = Console.ReadLine();

            byte[] data = Encoding.UTF8.GetBytes(mess);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Ответ сервера: "+ response);

            client.Close();
            Console.ReadKey();
        }
    }
}
