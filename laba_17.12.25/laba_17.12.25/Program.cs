using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace laba_17._12._25
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
        static void Main(string[] args)
        {
            MessageBox(IntPtr.Zero, "Информационное сообщение.", "Ура-ура!",0| 0x00000040);
            MessageBox(IntPtr.Zero, "Предупреждающее сообщение.", "Ура-ура!", 0 | 0x00000030);
            MessageBox(IntPtr.Zero, "Сообщение об ошибке.", "Ура-ура!", 0 | 0x00000010);
        }
    }
}
