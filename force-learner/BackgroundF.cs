using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace force_learner
{
    public partial class BackgroundF : Form
    {
        public BackgroundF()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settings s = new settings();
            s.Close = false;
            s.Save();

             
            this.WindowState = FormWindowState.Maximized;
            this.Enabled = true;
            MainF frm = new MainF();
            frm.Show();
            Thread th = new Thread(CheckForClose);
            th.Start();
        }

        static void CheckForClose()
        {
            while (true)
            {
                settings s = new settings();
                if(s.Close == true)
                {
                    Application.Exit();

                }
                Thread.Sleep(10);
            }
        }
    }
}
