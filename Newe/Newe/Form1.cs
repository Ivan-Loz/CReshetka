using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Newe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyProcess.StartInfo = new System.Diagnostics.
            ProcessStartInfo("explorer.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyProcess.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void stop_Click(object sender, EventArgs e)
        {
            MyProcess.CloseMainWindow();
            MyProcess.Close();
        }
    }
}
