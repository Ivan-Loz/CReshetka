using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace nas
{
    class BaseMessage
    {
        public virtual void Show()
        {
                  
        }
    }
    class HelloMessage : BaseMessage
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);


        public override void Show()
        {
            MessageBox(IntPtr.Zero, "Привет мир!", "Сообщение", 0);
        }
    }
    class GodbeyMessage : BaseMessage
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);


        public override void Show()
        {
            MessageBox(IntPtr.Zero, "Пока мир!", "Ура-ура!", 3);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //BaseMessage message = new HelloMessage();
            //message.Show();
            BaseMessage message1 = new GodbeyMessage();
            message1.Show();
        }
    }
}