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
using System.Management;
using System.IO;

namespace RealtimeCPURAMmonitor
{
    public partial class Form1 : Form
    {
        
        Timer CPU0 = new Timer();
        Timer CPU1 = new Timer();
        Timer CPU2 = new Timer();
        Timer CPU3 = new Timer();
        Timer RAM = new Timer();
        Timer[] CPU = new Timer[4];
        int clockSpeed,core = 0;
        string procName;
        bool button1_clicked = false;


        public Form1()
        {
            InitializeComponent();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            GetCpuCore();
            if (core > 0)
            {
                CPU0.Start();
                CPU0.Interval = 300;
                CPU0.Tick += new EventHandler(CPU0_tick);
            }
            if (core > 1)
            {
                CPU1.Start();
                CPU1.Interval = 300;
                CPU1.Tick += new EventHandler(CPU1_tick);
            }
            else {
                label2.Text = "CPU Jezgro 1 - NEMA";
            }
            if (core > 2)
            {
                CPU2.Start();
                CPU2.Interval = 300;
                CPU2.Tick += new EventHandler(CPU2_tick);
            }
            else
            {
                label3.Text = "CPU Jezgro 2 - NEMA";
            }
            if (core > 3)
            {
                CPU3.Start();
                CPU3.Interval = 300;
                CPU3.Tick += new EventHandler(CPU3_tick);
            }
            else
            {
                label4.Text = "CPU Jezgro 3 - NEMA";
            }
            RAM.Start();
            RAM.Interval = 300;
            RAM.Tick += new EventHandler(RAM_tick);

            progressBar5.Minimum = 0;
            progressBar5.Maximum = (int)GetTotalMemoryInMBytes();
            label7.Text = string.Format("Ukupno memorije : {0}MB", GetTotalMemoryInMBytes());
            label10.Text = string.Format("CPU Model : {0}", GetCPUName());
            label9.Text = string.Format("CPU Brzina : {0}MHz", GetCPUSpeed());
            label6.Text = string.Format("PC Hostname : {0}", Environment.MachineName);
            label8.Text = string.Format("Operativni sistem : {0}",GetOSFriendlyName());
            toolTip1.SetToolTip(button4, "Pogledajte izvorni kod na BitBucket-u.");
            toolTip2.SetToolTip(button3, "Pogledajte moj fejsbuk profil.");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!button1_clicked)
            {
                CPU0.Stop();
                CPU1.Stop();
                CPU2.Stop();
                CPU3.Stop();
                RAM.Stop();
                button1_clicked = true;
                button1.Text = "Pokreni praćenje";         
            }
            else {
                CPU0.Start();
                CPU1.Start();
                CPU2.Start();
                CPU3.Start();
                RAM.Start();
                button1_clicked = false;
                button1.Text = "Zaustavi praćenje";     
            }
        }     
        private void CPU0_tick(object sender, EventArgs e) {
            label1.Text = string.Format("CPU Jezgro 0 : {0}%",Math.Round(performanceCounter1.NextValue(),2));
        }
        private void CPU1_tick(object sender, EventArgs e)
        {
            label2.Text = string.Format("CPU Jezgro 1 : {0}%", Math.Round(performanceCounter2.NextValue(), 2));
        }
        private void CPU2_tick(object sender, EventArgs e)
        {
            label3.Text = string.Format("CPU Jezgro 2 : {0}%", Math.Round(performanceCounter3.NextValue(), 2));
        }
        private void CPU3_tick(object sender, EventArgs e)
        {
            label4.Text = string.Format("CPU Jezgro 3 : {0}%", Math.Round(performanceCounter4.NextValue(), 2));
        }
        private void RAM_tick(object sender, EventArgs e) {
            int tmp_calc = (int)(GetTotalMemoryInMBytes()) - ((int)performanceCounter5.NextValue());
            label5.Text = String.Format("Memorija u upotrebi : {0}MB", tmp_calc);
            progressBar5.Value = tmp_calc;
            
        }
        static ulong GetTotalMemoryInMBytes()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory/1024/1024;
        }
        private string GetCPUName()
        {
            using (ManagementObjectSearcher win32Proc = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (ManagementObject obj in win32Proc.Get())
                {
                    
                    procName = obj["Name"].ToString();
                }       
            }
            return procName;
        } 
        private int GetCPUSpeed()
        {
            using (ManagementObjectSearcher win32Proc = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (ManagementObject obj in win32Proc.Get())
                {
                    clockSpeed = Convert.ToInt32(obj["MaxClockSpeed"]);

                }
            }
            return clockSpeed;
        }
        private int GetCpuCore() {
            /*  using (ManagementObjectSearcher win32Proc = new ManagementObjectSearcher("select * from Win32_Processor"))
             {
                 foreach (ManagementObject obj in win32Proc.Get())
                 {
                     core += Convert.ToInt32(obj["NumberOfProcessors"].ToString());

                 }
             }
             return core; */
            core = Environment.ProcessorCount;
            return core;
        }
        public static string GetOSFriendlyName()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                result = os["Caption"].ToString();
                break;
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            start_prime95();
        }
        private int start_prime95() {
            string path = Path.Combine(Path.GetTempPath(), "prime95.exe");
            File.WriteAllBytes(path, RealtimeCPURAMmonitor.Properties.Resources.prime95);
            if (Process.Start(path) != null)
            {
                return 0;
            }
            else {
                return -1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://facebook.com/allexki");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://bitbucket.org/aleksandarbabic/cpumem");
        }
    }
}
