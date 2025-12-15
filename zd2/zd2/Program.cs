using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace zd2
{



    class Program
    {
        static int Attempts;
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
        static void Main(string[] args)
        {
            Console.WriteLine("Пк будет угадывать твоё число");
            while (true)
            {
                PlayGame();

                Console.WriteLine("Сыграть ещё? (y/n)");
                string answer = Console.ReadLine();
                if (!answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                    break;

            }
        }
        static void PlayGame()
        {
            int low = 0;
            int high = 100;
            bool guessed = false;
            while (!guessed)
            {
                int guess = (low + high) / 2;
                int result = AskUser(guess);
                if (result == 1)
                {
                    high = guess - 1;
                }
                else if (result == 2)
                {
                    low = guess + 1;
                }
                else if (result == 0)
                {
                    MessageBox(IntPtr.Zero, $"Угадал! Попытки: {Attempts}", "Ура-ура!", 0);
                    guessed = true;
                }
            }
        }
        static int AskUser(int guess)
        {
            string text = $"Ваше число {guess}?";
            string caption = "Угадывание числа";
            uint MB_YESNO = 0x00000004;
            uint MB_ICONQUESTION = 0x00000020;

            int respone = MessageBox(IntPtr.Zero, text, caption, MB_YESNO | MB_ICONQUESTION);
            if (respone == 6)
            
                return 0;

            uint MB_YESNOCANCEL = 0x00000003;
            int higher = MessageBox(IntPtr.Zero, "Загаданное число больше?", "Нука?", MB_YESNOCANCEL | MB_ICONQUESTION);

            if (higher == 6)
            {
                Attempts += 1;
                return 2;
            }
            if (higher == 7)
            {
                Attempts += 1;
                return 1;
            }
            MessageBox(IntPtr.Zero, "Игра остановлега", "СТОП!", 0);
            Environment.Exit(0);
            return -1;
        }
    }
}
