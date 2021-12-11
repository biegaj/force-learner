using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace force_learner
{
    public partial class MainF : Form
    {
        public MainF()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public string holdKeyVal = "";
        public string holdValVal = "";
        public int totalCount = 0;
        private void Form2_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            Initialise(false); // for comfort
        }

        public void Initialise(bool init)
        {
            Random rnd = new Random();
            WebClient wc = new WebClient();

            string wordList = wc.DownloadString("https://raw.githubusercontent.com/biegaj/force-learner/main/wordlist.txt"); //edit with own wordlist
            bool allowAccess = false;

            this.TopMost = true;

            List<string> totalKey = new List<string>();
            List<string> totalValue = new List<string>();

            foreach (string line in wordList.Split('\n'))
            {
                if (line.Length < 1) { }
                else
                {
                    string beforeColon = line.Substring(0, line.IndexOf(":"));
                    string afterColon = line.Substring(line.LastIndexOf(':') + 1);

                    totalKey.Add(beforeColon);
                    totalValue.Add(afterColon);
                }
            }

            CreateNewQuery(true);

            void CreateNewQuery(bool firstAllow = false)
            {
                label3.Text = $"{totalCount}/5";
                if (totalCount == 5)
                {
                    this.WindowState = FormWindowState.Minimized;

                    settings s = new settings();
                    s.Close = true;
                    s.Save();

                    Thread th = new Thread(Close);
                    th.Start();
                }

                if (firstAllow) { }
                else
                {
                    if (!init) { return; }
                }

                int holdRand = rnd.Next(0, totalKey.Count);
                label1.Text = lText(totalKey[holdRand]);
                holdKeyVal = totalKey[holdRand];
                holdValVal = totalValue[holdRand];
            }
        }

        string lText(string Word)
        {
            return $"What does '{Word}' mean?";
        }

        static void Close()
        {
            settings s = new settings();
            Thread.Sleep(s.timeoutI - 15000);
            if (s.popupS) { MessageBox.Show("Word checkup coming up in 15 seconds!"); }
            Thread.Sleep(15000);
            Application.Restart();
            Environment.Exit(0);
        }

        private void label1_SizeChanged(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Size.Width) / 2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string newVal = rgx.Replace(holdValVal, "");

            if (textBox1.Text == newVal)
            {
                textBox1.Text = "";
                totalCount++;
                Initialise(true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All credits go to Jakub B. @ www.github.com/biegaj");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingsF sf = new SettingsF();
            sf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
