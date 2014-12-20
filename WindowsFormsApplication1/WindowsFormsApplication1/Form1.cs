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

        string  a = "awal ngecas";  //flag
        public Form1()
        {
            InitializeComponent();


        }
        
        PowerStatus status = SystemInformation.PowerStatus;  //melihat persentase baterai
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Battery where Name='" + lblBatteryName.Text + "'");
            foreach (ManagementObject mo in mos.Get())
            {
                PowerStatus status = SystemInformation.PowerStatus; //memperlihatkan persentase
                BatteryIndicator.Value = (int)(status.BatteryLifePercent * 100);
                ChargeRemaining.Text = String.Format("{0}%", (status.BatteryLifePercent * 100));

                float b = status.BatteryLifePercent*100;
                //MessageBox.Show(a);

                if (a == "awal ngecas") //cek flag, apakah a nilainya "awal ngecas" ?
                {

                    if (b <= 98) //persentase baterai kurang dari 98% ? bisa diganti 100%
                    {
                        Process firstProc = new Process();
                        firstProc.StartInfo.FileName = "onae commandline.exe"; //panggil program onae 
                        firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        firstProc.StartInfo.Arguments = "on 0"; // charger on di port nomor 0
                        firstProc.EnableRaisingEvents = true;
                        firstProc.Start();

                        if (b == 98) { //apakah sudah penuh 98% ? bisa diganti 100%

                            a = "selesai ngecas"; //ganti flag menjadi "selesai ngecas"
                        }
                    }
                    
                }
                else if(a == "selesai ngecas"){ //bila flag menyatakan selesai ngecas
                    Process firstProc = new Process();
                    firstProc.StartInfo.FileName = "onae commandline.exe";
                    firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    firstProc.StartInfo.Arguments = "off 0"; //charger off
                    firstProc.EnableRaisingEvents = true;
                    firstProc.Start();

                    if (b > 30) //bila baterai masih di atas 30%, do nothing/charger tetap off
                    {


                    }
                    else {

                        a = "awal ngecas"; //bila di bawah 30%, maka charger on kembali
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

            //=======================================================================
            //ketika program mau ditutup, matikan semua port 
            Process firstProc = new Process(); 
            firstProc.StartInfo.FileName = "onae commandline.exe";
            firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            firstProc.StartInfo.Arguments = "off 0"; //matikan port 0
            firstProc.EnableRaisingEvents = true;

            firstProc.Start();
            //firstProc.WaitForExit();

            Process secondProc = new Process();
            secondProc.StartInfo.FileName = "onae commandline.exe";
            secondProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            secondProc.StartInfo.Arguments = "off 1"; //matikan port 1
            secondProc.EnableRaisingEvents = true;

            secondProc.Start();
            //secondProc.WaitForExit();


        }

        private void button1_Click(object sender, EventArgs e) //tombol manual on
        {
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = "onae commandline.exe";
            firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            firstProc.StartInfo.Arguments = "on " + comboBox1.SelectedIndex;
            firstProc.EnableRaisingEvents = true;

            firstProc.Start();
            //firstProc.WaitForExit();
        }

        private void button2_Click(object sender, EventArgs e) //tombol manual off
        {
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = "onae commandline.exe";
            firstProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            firstProc.StartInfo.Arguments = "off " + comboBox1.SelectedIndex;
            firstProc.EnableRaisingEvents = true;

            firstProc.Start();
            //firstProc.WaitForExit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //link di about
        {
            // Specify that the link was visited. 
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://idebeda.com");
        }

        
    }
}
