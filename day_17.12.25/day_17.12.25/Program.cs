using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace day_17._12._25
{

    class BaseWindowCloser
    {
        public virtual void CloseTarget(string Window)
        {

        }
    }

    class NotepadCloser : BaseWindowCloser
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName,string lpindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd,uint Msg,IntPtr wParam,IntPtr lParam);

        uint WM_CLOSE = 0x0010;

        public override void CloseTarget(string Window)
        {
            
            IntPtr hWnd = FindWindow(Window, null);
            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("Окно не найдено!");
                return;
            }
            Console.WriteLine("Окно найдено. отправка WM_CLOSE");
            SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
                string Window;
                Console.WriteLine("Введите название окна");
                Window = Console.ReadLine();
                BaseWindowCloser closer = new NotepadCloser();
                closer.CloseTarget(Window);
                Console.WriteLine("Программа всё!");
                Console.Read();
        }
    }
}
