using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OSLab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ProcessManager.instance().processesChanged += new EventHandler(Form1_processesChanged);
            InitializeComponent();
            Form1_processesChanged(this, new EventArgs());   
        }

        void Form1_processesChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < ProcessManager.instance().Count; ++i)
            {
                MyProcess prc = ProcessManager.instance().At(i);
                listBox1.Items.Add("{location = " + prc.Location + "; length = " + prc.Length + ";}");
            }

            label1.Text = "Free memory: " + ProcessManager.instance().FreeMemory;
            label2.Text = "Total memory: " + ProcessManager.instance().MemorySize;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessManager.instance().CreateProcess(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessManager.instance().CreateProcess(20);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProcessManager.instance().CreateProcess(40);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProcessManager.instance().CreateProcess(55);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProcessManager.instance().CreateProcess(70);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                ProcessManager.instance().DeleteProcess(listBox1.SelectedIndex);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProcessManager.instance().Defragment();
        }
    }
}
