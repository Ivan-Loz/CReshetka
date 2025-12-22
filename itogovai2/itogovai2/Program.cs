using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;

namespace itogovai2
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        static void Main(string[] args)
        {
            RunApps();
        }
        static void RunApps()
        {
            Console.Title = "Запуск приложения";
            while (true)
            {
                Console.Clear();

                Console.WriteLine("\n1. Запустить блокнот");
                switch (Console.ReadLine())
                {
                    case "1": Console.Clear(); string name = Console.ReadLine(); int answer = MessageBox(IntPtr.Zero, $"Точно запустить программу {name}", "Вопросик?", 4); if (answer == 6) { Process NotepadProc = Process.Start(name); Console.WriteLine($"Запущен {name}. PID: " + NotepadProc.Id); NotepadProc.WaitForExit(); MessageBox(IntPtr.Zero, $"Ну как-бы всё!", "???", 0);} else { return; } return;
                    default: Console.Clear(); Console.Title = "Ошибка!"; Console.WriteLine("неправильный ввод!"); Console.ReadKey(); return;
                }
            }
        }

    }
}