using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;


namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {

        string  a = "awal ngecas";
        public Form1()
        {
            InitializeComponent();


        }
        
        PowerStatus status = SystemInformation.PowerStatus;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Battery where Name='" + lblBatteryName.Text + "'");
            foreach (ManagementObject mo in mos.Get())
            {
                PowerStatus status = SystemInformation.PowerStatus;
                BatteryIndicator.Value = (int)(status.BatteryLifePercent * 100);
                ChargeRemaining.Text = String.Format("{0}%", (status.BatteryLifePercent * 100));

                float b = status.BatteryLifePercent*100;
                //MessageBox.Show(a);

                if (a == "awal ngecas")
                {

                    if (b <= 98)
                    {
                        Process firstProc = new Process();
                        firstProc.StartInfo.FileName = "onae commandline.exe";
                        firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        firstProc.StartInfo.Arguments = "on 0";
                        firstProc.EnableRaisingEvents = true;
                        firstProc.Start();

                        if (b == 98) {

                            a = "selesai ngecas";
                        }
                    }
                    
                }
                else if(a == "selesai ngecas"){
                    Process firstProc = new Process();
                    firstProc.StartInfo.FileName = "onae commandline.exe";
                    firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    firstProc.StartInfo.Arguments = "off 0";
                    firstProc.EnableRaisingEvents = true;
                    firstProc.Start();

                    if (b > 30)
                    {


                    }
                    else {

                        a = "awal ngecas";
                    }
                                    
                }
                
            }
                   
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Battery");
            foreach (ManagementObject mo in mos.Get())
            {
                lblBatteryName.Text = mo["Name"].ToString();
            }


        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            //switch (MessageBox.Show(this, "Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo))
            //{
            //   case DialogResult.No:
            //        e.Cancel = true;
            //        break;
            //    default:
            //         break;
            // }
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = "onae commandline.exe";
            firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            firstProc.StartInfo.Arguments = "off 0";
            firstProc.EnableRaisingEvents = true;

            firstProc.Start();
            //firstProc.WaitForExit();

            Process secondProc = new Process();
            secondProc.StartInfo.FileName = "onae commandline.exe";
            secondProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            secondProc.StartInfo.Arguments = "off 1";
            secondProc.EnableRaisingEvents = true;

            secondProc.Start();
            //secondProc.WaitForExit();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = "onae commandline.exe";
            firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            firstProc.StartInfo.Arguments = "on " + comboBox1.SelectedIndex;
            firstProc.EnableRaisingEvents = true;

            firstProc.Start();
            //firstProc.WaitForExit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = "onae commandline.exe";
            firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            firstProc.StartInfo.Arguments = "off " + comboBox1.SelectedIndex;
            firstProc.EnableRaisingEvents = true;

            firstProc.Start();
            //firstProc.WaitForExit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited. 
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://idebeda.com");
        }

        
    }
}