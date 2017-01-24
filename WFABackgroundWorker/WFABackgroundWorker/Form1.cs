using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFABackgroundWorker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            lblPercentage.Visible = false;
        }

        private void WasteCPUCycles()
        {
            DateTime startTime = DateTime.Now;
            double value = Math.E;
            while (DateTime.Now < startTime.AddMilliseconds(100))
            {
                value /= Math.PI;
                value *= Math.Sqrt(2);
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Background worker argument: " + (e.Argument ?? "null"));

            for (int i = 1; i <= 100; i++)
            {
                WasteCPUCycles();
                backgroundWorker.ReportProgress(i);

                if (backgroundWorker.CancellationPending)
                {
                    Console.WriteLine("Cancelled");
                    break;
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblPercentage.Text = string.Format("{0}%", e.ProgressPercentage);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            lblPercentage.Visible = true;
            if (!useBackgroundWorker.Checked)
            {
                for (int i = 1; i <= 100; i++)
                {
                    lblPercentage.Text = string.Format("{0}%",i);
                    WasteCPUCycles();
                    progressBar1.Value = i;
                    Refresh(); //pode utilizar tanto o Refresh() como Update()
                }
                btnStart.Enabled = true;
            }
            else
            {
                btnCancel.Enabled = true;
                backgroundWorker.RunWorkerAsync(new Guy("Felipe", 22, 150));
                //Saída no console
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }
    }
}
