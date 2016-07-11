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
            RAM.Interval = 300;
            RAM.Tick += new EventHandler(RAM_tick);
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
            label1.Text = string.Format("CPU Core 0 : {0}%",Math.Round(performanceCounter1.NextValue(),2));
        }
        private void CPU1_tick(object sender, EventArgs e)
        {
            label2.Text = string.Format("CPU Core 1 : {0}%", Math.Round(performanceCounter2.NextValue(), 2));
        }
        private void CPU2_tick(object sender, EventArgs e)
        {
            label3.Text = string.Format("CPU Core 2 : {0}%", Math.Round(performanceCounter3.NextValue(), 2));
        }
        private void CPU3_tick(object sender, EventArgs e)
        {
            label4.Text = string.Format("CPU Core 3 : {0}%", Math.Round(performanceCounter4.NextValue(), 2));
        }
        private void RAM_tick(object sender, EventArgs e) {
            label5.Text = String.Format("RAM : {0}", (performanceCounter5.NextValue()));
            progressBar5.Value = (int)performanceCounter5.NextValue();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
