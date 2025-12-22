using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace itog3
{
    class Program
    {
        class BasePr
        {
           public virtual void StartPr()
           {

           }
            public virtual void StoptPr()
            {
    
            }
            public virtual void CheckPr()
            {
             

            }
        }
        class Notepad : BasePr
        {
            public override void StartPr()
            {
                Process NotepadProc = Process.Start("notepad.exe"); 
                Console.WriteLine("Запущен блокнот. PID: " + NotepadProc.Id); 
            }
            public override void StoptPr()
            {
                Console.Title = "Убить процесс";

                Process[] processes = Process.GetProcessesByName("notepad");
                foreach (Process proc in processes)
                {
                    proc.CloseMainWindow();
                }

                Console.WriteLine("процесс убит!");
                Console.ReadKey();
            }
            public override void CheckPr()
            {
                Process[] processesByName = Process.GetProcessesByName("notepad");
                if (processesByName.Length > 0)
                {
                    Console.WriteLine($"Приложение notepad найдено (ProcessName).");
                    foreach (var p in processesByName)
                    {
                        Console.WriteLine($" - PID: {p.Id}, MainModule: {p.MainModule?.FileName ?? "N/A"}");
                    }
                }
                else
                {
                    Console.WriteLine($"Приложение notepad не найдено (ProcessName).");
                }

            }
        }
        static void Main(string[] args)
        {
            BasePr process = new Notepad();
            process.StartPr();
            process.CheckPr();
            process.StoptPr();
            Console.ReadLine();
        }
    }
}
