using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace SYPER
{
    class Program
    {
        static Process childProcess = null;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n1. Запустить процесс");
                Console.WriteLine("\n2. Показать информацию о процессе");
                Console.WriteLine("\n3. Прочитать вывод процесса");
                Console.WriteLine("\n4. Запустить процесс");
                Console.WriteLine("\n5. Выход");
                Console.Write("\nВыберите пункт: ");

                var choice = Console.ReadLine();

                switch (choice) {
                    case "1": SrartProcess(); break;
                    case "2": ShowInfo(); break;
                    case "3": ReadOutput(); break;
                    case "4": StopProcess(); break;
                    case "5": return;
                    default: Console.WriteLine("Ошибся!"); break;
                }

            }
        }
        static void SrartProcess(){
            if (childProcess != null && !childProcess.HasExited)
            {
                Console.WriteLine("Процесс уже запущен");
                return;
            }
            Console.WriteLine("Имя!(пример: ping)");
            string fail = Console.ReadLine();

            Console.WriteLine("Аргумент!(пример: 127.0.0.1 -n 4)");
            string args = Console.ReadLine();

            var startInfo = new ProcessStartInfo
            {
                FileName = fail,
                Arguments = args,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                childProcess = Process.Start(startInfo);
                Console.WriteLine("Процесс запущен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска{ex.Message}");
            }
        }
        static void ShowInfo()
        {
            if(childProcess == null)
            {
                Console.WriteLine($"Процесс не был запущен");
                return;
            }
            Console.WriteLine($"Индификатор: {childProcess.Id}");
            Console.WriteLine($"HasExited: {childProcess.HasExited}");
            if (!childProcess.HasExited)
            {
                Console.WriteLine($"Время работы: {childProcess.TotalProcessorTime}");
                Console.WriteLine($"Память: {childProcess.WorkingSet64} байт");
            }
        }

        static void ReadOutput()
        {
            if (childProcess == null)
            {
                Console.WriteLine($"Процесс не был запущен");
                return;
            }

            try
            {
                string output = childProcess.StandardOutput.ReadToEnd();
                Console.WriteLine("Вывод процесса: ");
                Console.WriteLine(output);
                LogProcessOutput(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void StopProcess()
        {
            try
            {
                if (!childProcess.HasExited)
                {
                    childProcess.Kill();
                    childProcess.WaitForExit();
                    Console.WriteLine($"Процесс завершён");
                }
                else
                {
                    Console.WriteLine($"Процесс уже завершён");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка остановки: {ex.Message}");
            }
        }
        static void LogProcessOutput(string output)
        {
            try
            {
                using(StreamWriter writer = new StreamWriter("log.txt", true))
                {
                    writer.WriteLine($"[{DateTime.Now}] Вывод процесса с ID {childProcess.Id}");
                    writer.WriteLine(output);
                    writer.WriteLine("----------------------------------------------------------------------------------");
                }
                Console.WriteLine($"Вывод процесса записан в лог");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
            }
        }
    }
}
