using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace laba3_17._12._25
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        static void Main(string[] args)
        {
            int ask = MessageBox(IntPtr.Zero, "Надо?", "Ура-ура!", 4 | 0x00000040);
            if(ask == 6){
                Process process = Process.Start("notepad.exe");
                process.WaitForExit();
                Console.WriteLine("Всё!");
                Console.Read();
            }
            else if (ask == 7)
            {
                Console.WriteLine("Ой!");
                Console.Read();
            }

        }
    }
}
