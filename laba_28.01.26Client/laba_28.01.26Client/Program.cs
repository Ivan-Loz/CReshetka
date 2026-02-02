using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        TcpClient client = new TcpClient("192.168.0.47", 5000);
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        Console.WriteLine("Подключено к серверу. Ожидание цитат...");

        while (true)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0) break;

            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine(message);
        }

        client.Close();
    }
}
