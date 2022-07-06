using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Memory;

namespace ETS2Trainer
{
    public partial class MainForm : Form
    {
        Mem m = new Mem();

        public MainForm()
        {
            InitializeComponent();
        }

        bool ProcOpen = false;

        private void MainForm_Shown(object sender, EventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcOpen = m.OpenProcess("eurotrucks2");

            Thread.Sleep(100);
            BGWorker.ReportProgress(0);
        }

        private void BGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProcOpen)
            {
                ProcOpenLabel.ForeColor = Color.Green;
                ProcOpenLabel.Text = "Yes";
            }
            else
            {
                ProcOpenLabel.ForeColor = Color.Red;
                ProcOpenLabel.Text = "N/A";
                return; // do not continue if process is not available/open
            }
        }

        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void SetMoney_Click(object sender, EventArgs e)
        {
            if (MoneyAmount.Text == "")
            {
                MoneyAmount.Text = "Type in a number.";
            }
            else
            {
                // Game Version 1.39.2.1s
                // m.WriteMemory("eurotrucks2.exe+0170D110,10,10", "int", MoneyAmount.Text);

                // Public Beta 1.41
                // m.WriteMemory("eurotrucks2.exe+0195BF28,10,10", "int", MoneyAmount.Text);

                // Public Beta 1.42 Build: 7503285
                //m.WriteMemory("eurotrucks2.exe+01965D38,10,10", "int", MoneyAmount.Text);

                // Game Version XXX
                //m.WriteMemory("eurotrucks2.exe+01A0E348,10,10", "int", MoneyAmount.Text);

                // Game Version 1.44.1.10s Build: 8879813
                m.WriteMemory("eurotrucks2.exe+01AEA4B8,10,10", "int", MoneyAmount.Text);
                MoneyAmount.Text = "";
            }
        }

        private void SetEXP_Click(object sender, EventArgs e)
        {
            if (EXPAmount.Text == "")
            {
                EXPAmount.Text = "Type in a number.";
            }
            else
            {
                // Game Version 1.38.X
                // m.WriteMemory("eurotrucks2.exe+18E849B7D9C,FC,0,740,278", "int", EXPAmount.Text);

                // Public Beta 1.41
                // m.WriteMemory("eurotrucks2.exe+0195BF28,195C", "int", EXPAmount.Text);

                // Public Beta 1.42 Build: 7503285
                // m.WriteMemory("eurotrucks2.exe+01965D38,195C", "int", EXPAmount.Text);

                // Game Version 1.44.1.10s Build: 8879813
                m.WriteMemory("eurotrucks2.exe+01AEA4B8,195C", "int", EXPAmount.Text);
                EXPAmount.Text = "";
            }
        }

        // Bypass Garage Upgrade Check Button
        private void rgch_CheckedChanged(object sender, EventArgs e)
        {
            if (rgch.Checked)
            {
                m.WriteMemory("eurotrucks2.exe+8315F8", "bytes", "83 79 10 00");
            } 
            else
            {
                m.WriteMemory("eurotrucks2.exe+8315F8", "bytes", "48 39 41 10");
            }
        }

        // Reverse Money Button
        private void rmd_CheckedChanged(object sender, EventArgs e)
        {
            if (rmd.Checked)
            {

                m.WriteMemory("eurotrucks2.exe+58485E", "bytes", "48 01 D8");
            }
            else
            {
                m.WriteMemory("eurotrucks2.exe+58485E", "bytes", "48 29 C3");
            }
        }

        // Bypass Garage Buy Check
        private void gbch_CheckedChanged(object sender, EventArgs e)
        {
            if (gbch.Checked)
            {
                m.WriteMemory("eurotrucks2.exe+786A50", "bytes", "83 7A 10 00");
            } 
            else
            {
                m.WriteMemory("eurotrucks2.exe+786A50", "bytes", "48 39 4A 10");
            }
        }
    }
}

