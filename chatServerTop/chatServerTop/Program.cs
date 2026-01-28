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

        static Dictionary<TcpClient, string> clientNames =
            new Dictionary<TcpClient, string>();

        static Dictionary<TcpClient, DateTime> lastMessageTime =
            new Dictionary<TcpClient, DateTime>();

        static List<string> messageHistory =
            new List<string>();

        const int MaxHistory = 10;

        static void Main()
        {
            TcpListener server =
                new TcpListener(IPAddress.Any, 5000);

            server.Start();
            Console.WriteLine("Чат-сервер запущен...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);

                Console.WriteLine("Новый клиент подключён.");
                Console.WriteLine($"Подключено клиентов: {clients.Count}");

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
                // --- Получение имени ---
                int nameBytes = stream.Read(buffer, 0, buffer.Length);
                string name = Encoding.UTF8.GetString(buffer, 0, nameBytes).Trim();

                clientNames[client] = name;
                lastMessageTime[client] = DateTime.MinValue;

                string joinMessage = $"[Система] {name} вошёл в чат\n";
                BroadcastSystemMessage(joinMessage);

                // --- Отправка истории ---
                foreach (string msg in messageHistory)
                {
                    byte[] historyData = Encoding.UTF8.GetBytes(msg);
                    stream.Write(historyData, 0, historyData.Length);
                }

                // --- Основной цикл ---
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;

                    string message =
                        Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                    // --- Проверка пустого сообщения ---
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        SendToClient(client, "Пустые сообщения запрещены\n");
                        continue;
                    }

                    // --- Антиспам ---
                    DateTime now = DateTime.Now;
                    TimeSpan diff = now - lastMessageTime[client];

                    if (diff.TotalSeconds < 3)
                    {
                        SendToClient(client,
                            "Сообщения можно отправлять не чаще одного раза в 3 секунды\n");
                        continue;
                    }

                    lastMessageTime[client] = now;

                    // --- Команды ---
                    if (message.StartsWith("/"))
                    {
                        HandleCommand(client, message);
                        continue;
                    }

                    string fullMessage =
                        $"{clientNames[client]}: {message}\n";

                    BroadcastMessage(fullMessage, client);
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
                        $"[Система] {clientNames[client]} покинул чат\n";
                    BroadcastSystemMessage(leftMessage);
                }

                clients.Remove(client);
                clientNames.Remove(client);
                lastMessageTime.Remove(client);
                client.Close();

                Console.WriteLine($"Подключено клиентов: {clients.Count}");
            }
        }

        static void HandleCommand(TcpClient client, string message)
        {
            NetworkStream stream = client.GetStream();

            if (message == "/users")
            {
                string users = "Пользователи:\n";
                foreach (string name in clientNames.Values)
                    users += "- " + name + "\n";

                SendToClient(client, users);
            }
            else if (message == "/time")
            {
                string time =
                    "[Система] Серверное время: " +
                    DateTime.Now.ToString("HH:mm:ss") + "\n";

                BroadcastSystemMessage(time);
            }



            else if (message == "/help")
            {
                string help =
                    "/users - список пользователей\n" +
                    "/pm имя сообщение - личное сообщение\n" +
                    "/help - список команд\n";

                SendToClient(client, help);
            }
        }

        static void BroadcastMessage(string message, TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            foreach (TcpClient client in clients)
            {
                if (client != sender)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                }
            }

            messageHistory.Add(message);
            if (messageHistory.Count > MaxHistory)
                messageHistory.RemoveAt(0);

            Console.Write(message);
        }

        static void BroadcastSystemMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            foreach (TcpClient client in clients)
            {
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
            }

            Console.Write(message);
        }

        static void SendToClient(TcpClient client, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
        }
    }
}