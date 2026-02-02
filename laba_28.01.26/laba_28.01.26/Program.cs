using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    class Program
    {
        static List<TcpClient> clients = new List<TcpClient>();

        static List<string> quotes =
            new List<string>
            {
                "Освободите себя от надежды, что море когда-нибудь успокоится. Мы должны научиться плыть при сильном ветре.",
                "Жизнь коротка, искусство вечно, случайные обстоятельства скоропреходящи, опыт обманчив, суждения трудны.",
                "Бесполезные люди живут только для того, чтобы есть и пить. Достойные люди едят и пьют только для того, чтобы жить.",
                "То, что ты не хочешь иметь завтра, отбрось сегодня, а то, что хочешь иметь завтра, приобретай сегодня.","Если в жизни нет удовольствия, то должен быть хоть какой-нибудь смысл.",
                "Разделите каждую сложную задачу на столько частей, сколько возможно и необходимо для её решения.",
                "Существовать — это быть в гармонии",
                "Суди о человеке больше по его вопросам, чем по его ответам.",
                "То, что хочешь ты зажечь в других, должно гореть в тебе самом.",
                "Мало людей мыслят, но все хотят иметь мнение.",
                "Научить человека быть счастливым — нельзя, но воспитать его так, чтобы он был счастливым, можно."
            };
        static Dictionary<TcpClient, string> clientNames =
            new Dictionary<TcpClient, string>();
        static void Main()
        {
            TcpListener server =
                new TcpListener(IPAddress.Any, 5000);

            server.Start();
            Console.WriteLine("Напеши quote");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                Thread clientThread =
                    new Thread(HandleClient);

                clientThread.Start(client);
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {

                int nameBytes = stream.Read(buffer, 0, buffer.Length);
                string name = Encoding.UTF8.GetString(buffer, 0, nameBytes).Trim();

                clientNames[client] = name;

                string joinMessage = $"[Система] {name} вошёл к нам\n";
                Console.WriteLine(joinMessage);

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;

                    string message =
                        Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                    if (message == "quote")
                    {
                        Random rand = new Random();
                        int qe = rand.Next(0, quotes.Count);
                        string qote = $"Цитатка: {quotes[qe]}";
                        SendToClient(client, qote);
                        continue;
                    }
                }
            }
            catch
            {
                // игнорируем ошибки соединения
            }
            finally
            {
                if (clientNames.ContainsKey(client))
                {
                    string leftMessage =
                        $"[Система] {clientNames[client]} покинул нас\n";
                    Console.WriteLine(leftMessage);
                }

                clients.Remove(client);
                clientNames.Remove(client);
                client.Close();

            }
        }
        static void SendToClient(TcpClient client, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
        }
    }
}