using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;

namespace itog1
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Title = "Меню";
                Console.Clear();
                Console.WriteLine("\n1. Показать список процессов");
                Console.WriteLine("2. Проверка процесса (запушен или нет)");
                Console.WriteLine("5. Выйти");
                switch (Console.ReadLine())
                {
                    case "1": Console.Clear(); Console.WriteLine("Введите количество милисекунд, через которое будет обновляться список"); int timer = int.Parse(Console.ReadLine()); ShowProcList(timer); break;
                    case "2": Console.Clear(); string prName = Console.ReadLine(); bool act = ShowProcess(prName); if (act == true) { MessageBox(IntPtr.Zero, $"Процесс {prName} активен", "Процесс", 0) ; } else { MessageBox(IntPtr.Zero, $"Процесс {prName} не найден", "Процесс", 0); } Console.ReadLine(); break;
                    case "5": return;
                    default: Console.Clear(); Console.Title = "Ошибка!"; Console.WriteLine("неправильный ввод!"); Console.ReadKey(); break;
                }
            }
        }
        static void ShowProcList(int UpdateTime)
        {
            Console.Title = "Показать список процессов";
            while (true)
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Process[] processes = Process.GetProcesses();
                    foreach (Process pr in processes)
                    {
                        Console.WriteLine($"{pr.Id}. {pr.ProcessName}");
                    }
                    Thread.Sleep(UpdateTime);
                }
        }
        static bool ShowProcess(string prName)
        {
            Process[] processes = Process.GetProcessesByName(prName);
            return processes.Length > 0;
        }
    }
}