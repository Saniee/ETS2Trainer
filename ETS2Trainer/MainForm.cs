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
                m.WriteMemory("eurotrucks2.exe+01958BA8,10,10", "int", MoneyAmount.Text);
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
                //m.WriteMemory("eurotrucks2.exe+18E849B7D9C,FC,0,740,278", "int", EXPAmount.Text);

                // Public Beta 1.41
                m.WriteMemory("eurotrucks2.exe+01958BA8,195C", "int", EXPAmount.Text);
            }
        }

        private void rgch_CheckedChanged(object sender, EventArgs e)
        {
            if (rgch.Checked)
            {
                m.WriteMemory("eurotrucks2.exe+A361E8", "bytes", "83 79 10 10");
            } 
            else
            {
                m.WriteMemory("eurotrucks2.exe+A361E8", "bytes", "48 39 41 10");
            }
        }

        private void rmd_CheckedChanged(object sender, EventArgs e)
        {
            if (rmd.Checked)
            {
                m.WriteMemory("eurotrucks2.exe+6B782E", "bytes", "48 01 D8");
            }
            else
            {
                m.WriteMemory("eurotrucks2.exe+6B782E", "bytes", "48 2B C3");
            }
        }
    }
}

