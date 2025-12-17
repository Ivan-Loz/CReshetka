using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace laba2_17._12._25
{
    class Message
    {

        public virtual void ShowMessage()
        {

        }
    }
    class Hello : Message
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public override void ShowMessage()
        {
            MessageBox(IntPtr.Zero, "Привет, мир!", "Ура-ура!", 0 | 0x00000040);
        }
    }
    class Bye : Message
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public override void ShowMessage()
        {
            MessageBox(IntPtr.Zero, "Пока!", "Ура-ура!", 0 | 0x00000040);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {     
            Message message = new Hello();
            Message message1 = new Bye();

            message.ShowMessage();
            message1.ShowMessage();

        }
    }
}
