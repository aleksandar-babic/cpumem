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

namespace RealtimeCPURAMmonitor
{
    public partial class Form1 : Form
    {
        
        Timer CPU0 = new Timer();
        Timer CPU1 = new Timer();
        Timer CPU2 = new Timer();
        Timer CPU3 = new Timer();
        Timer RAM = new Timer(); 
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //once_loaded();
            CPU0.Start();
            CPU0.Interval = 300;
            CPU0.Tick += new EventHandler(CPU0_tick);
            CPU1.Start();
            CPU1.Interval = 300;
            CPU1.Tick += new EventHandler(CPU1_tick);
            CPU2.Start();
            CPU2.Interval = 300;
            CPU2.Tick += new EventHandler(CPU2_tick);
            CPU3.Start();
            CPU3.Interval = 300;
            CPU3.Tick += new EventHandler(CPU3_tick);
            RAM.Start();
        }
        private void once_loaded () {
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CPU0.Start();
            CPU1.Start();
            CPU2.Start();
            CPU3.Start();
            RAM.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CPU0.Stop();
            CPU1.Stop();
            CPU2.Stop();
            CPU3.Stop();
            RAM.Stop();
        }
        private void CPU0_tick(object sender, EventArgs e) {
            label1.Text = string.Format("CPU Core 0 - Usage : {0}%",performanceCounter1.NextValue());
            progressBar1.Value = (int)performanceCounter1.NextValue();
        }
        private void CPU1_tick(object sender, EventArgs e)
        {
            label2.Text = string.Format("CPU Core 1 - Usage : {0}%", performanceCounter2.NextValue());
            progressBar2.Value = (int)performanceCounter2.NextValue();
        }
        private void CPU2_tick(object sender, EventArgs e)
        {
            label3.Text = string.Format("CPU Core 2 - Usage : {0}%", performanceCounter3.NextValue());
            progressBar3.Value = (int)performanceCounter3.NextValue();
        }
        private void CPU3_tick(object sender, EventArgs e)
        {
            label4.Text = string.Format("CPU Core 3 - Usage : {0}%", performanceCounter4.NextValue());
            progressBar4.Value = (int)performanceCounter4.NextValue();
        }
    }
}
