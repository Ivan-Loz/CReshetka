using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace New
{
    class Program
    {
        static void AllProc()
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process pr in processes)
            {
                Console.WriteLine($"PID: {pr.Id}, Name: {pr.ProcessName}, что-то: {pr.EnableRaisingEvents}");
            }
        }
        static void ShowProc(int pid)
        {
            Process pr = Process.GetProcessById(pid);
            Console.WriteLine($"Имя: {pr.ProcessName}");
            Console.WriteLine($"Приоритет: {pr.BasePriority}");
            Console.WriteLine("");
            //Console.WriteLine($"Запущен: {pr.StartTime}");
            //Console.WriteLine($"Загружено модулей: {pr.Modules.Count}");

        }
        static void StartNotepad()
        {
            Process pr = Process.Start("notepad.exe");
            Console.WriteLine("Запущен Notepad. PID = "+ pr.Id);
        }
        static void StartNotepadAndWait()
        {
            Process pr = new Process();
            pr.StartInfo.FileName = "notepad.exe";
            pr.EnableRaisingEvents = true;
            pr.Exited += (s, e) =>
            {
                Console.WriteLine($"Notepad закрыт. Код выхода: {pr.ExitCode}");
            };
            pr.Start();
            pr.WaitForExit();
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"1.Показать процессы");
                Console.WriteLine($"2.Показать");
                Console.WriteLine("3.Открыть Notepad");
                Console.WriteLine("4.Открыть Notepad и закрыть");
                Console.WriteLine($"0.Выход");
                Console.Write("Выберите пункт: ");
                

                switch (Console.ReadLine())
                {
                    case "1": AllProc(); break;
                    case "2":
                        Console.WriteLine($"Айди:");
                        int Id;
                        Id = int.Parse(Console.ReadLine());
                        ShowProc(Id); 
                        break;
                    case "3": StartNotepad(); break;
                    case "4": StartNotepadAndWait(); break;
                    case "0": return;
                    default: Console.WriteLine("Ошибка ввода."); break;
                }
            }
        }
    }
}
