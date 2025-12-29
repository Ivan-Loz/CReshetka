using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace NgProdgect
{
    static class WinApiHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public const int IDYES = 6;

        private const uint MB_OK = 0x00000000;
        private const uint MB_YESNO = 0x00000004;
        private const uint MB_ICONINFORMATION = 0x00000040;
        private const uint MB_ICONWARNING = 0x00000030;
        private const uint MB_ICONQUESTION = 0x00000020;

        public static void ShowInfo(string text, string caption) => MessageBox(IntPtr.Zero, text,
    caption, MB_OK | MB_ICONINFORMATION);
        public static void ShowWarning(string text, string caption) => MessageBox(IntPtr.Zero,
    text, caption, MB_OK | MB_ICONWARNING);
        public static int ShowYesNo(string text, string caption) => MessageBox(IntPtr.Zero, text,
    caption, MB_YESNO | MB_ICONQUESTION);
    }
    class Player
    {
        public string Name { get; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }
    class RiddleQuest : Quest
    {
        public RiddleQuest() { Title = "Новогодняя загадка"; }

        public override int Execute()
        {
            Console.WriteLine("Зимой и летом одним цветом.");
            Console.Write("Ваш ответ: ");
            string answer = Console.ReadLine().ToLower();

            if (answer == "елка" || answer == "ёлка")
            {
                WinApiHelper.ShowInfo("Верно!", Title);
                return 10;
            }
            WinApiHelper.ShowWarning("Неверный ответ.", Title);
            return 0;
        }
    }
    abstract class Quest
    {
        public string Title { get; protected set; }
        public abstract int Execute();
    }
    class MemoryQuest : Quest
    {
        public MemoryQuest() { Title = "Испытание на внимательность"; }

        public override int Execute()
        {
            string sequence = "Снеговик Саня ёжик";
            WinApiHelper.ShowInfo(sequence, "Запомните последовательность");

            Thread.Sleep(5000);
            Console.Clear();

            Console.Write("Введите последовательность: ");
            string input = Console.ReadLine().ToUpper();

            if (input == sequence)
            {
                WinApiHelper.ShowInfo("Отличная память!", Title);
                return 10;
            }
            WinApiHelper.ShowWarning("Ошибка.", Title);
            return 0;
        }
    }
    class LoveeQuest : Quest
    {
        public LoveeQuest() { Title = "Новогодняя загадка"; }

        public override int Execute()
        {
            Console.WriteLine("Кто красный и приносит подарки?");
            Console.Write("Ваш ответ: ");
            string answer = Console.ReadLine().ToLower();

            if (answer == "дед мороз" || answer == "Дед Мороз")
            {
                WinApiHelper.ShowInfo("Верно!", Title);
                return 10;
            }
            WinApiHelper.ShowWarning("Неверный ответ.", Title);
            return 0;
        }
    }
    class FinalChoiceQuest : Quest
    {
        public FinalChoiceQuest() { Title = "Последний выбор"; }

        public override int Execute()
        {
            int result = WinApiHelper.ShowYesNo("Пожертвовать подарками ради спасения праздника ? ", Title); 
            if (result == WinApiHelper.IDYES)
            {
                Console.WriteLine("Вы спасли праздник.");
                return 10;
            }
            Console.WriteLine("Праздник прошёл без чуда.");
            return 0;
        }
    }
    class Program
    {
       public void SaveScore(int score,string Name)
        {
            string Score = "Score.txt";
            string[] lines = File.ReadAllLines(Score);
            for (int i =0;i < lines.Length; i++)
            {
                if (lines[i] == Name)
                {
                    File.WriteAllText(Score,lines[i+1] + score);
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(Score, false, Encoding.UTF8))
                    {
                        sw.WriteLine(Name);
                        sw.WriteLine(score);
                    }
                }
            }

        }
        static void Main(string[] args)
        {
            int start = WinApiHelper.ShowYesNo("Начать новогодний квест?", "Новогодний квест"); 
            if (start != WinApiHelper.IDYES) return;

            Console.Clear();
            Console.Write("Введите имя игрока: ");
            Player player = new Player(Console.ReadLine());

            Quest[] quests = { new RiddleQuest(), new MemoryQuest(), new FinalChoiceQuest(),new LoveeQuest() };

            foreach (Quest quest in quests)
            {
                Console.Clear();
                Console.WriteLine($"Квест: {quest.Title}\n");
                player.Score += quest.Execute();
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }

            Console.Clear();
            Console.WriteLine($"Игра завершена, {player.Name}!");
            Console.WriteLine($"Ваш итоговый счёт: {player.Score}");

          /**  string score = "Score.txt";
            string[] lines = File.ReadAllLines(score);
            for (int i = 0; i <= lines.Length; i++)
            {
                if (lines[i] == player.Name)
                {
                    File.WriteAllText(score, lines[i + 1] + player.Score);
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(score, false, Encoding.UTF8))
                    {
                        sw.WriteLine(player.Name);
                        sw.WriteLine(player.Score);
                    }
                }
            }**/

            WinApiHelper.ShowInfo("Новогодний квест завершён.\nСпасибо за игру!", "Игра окончена"); 
        }
    }
}
