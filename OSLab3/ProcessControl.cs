using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OSLab3
{
    public partial class ProcessControl : UserControl
    {
        public ProcessControl()
        {
            ProcessManager.instance().processesChanged += new EventHandler(ProcessControl_processesChanged);
            InitializeComponent();
        }

        void ProcessControl_processesChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void ProcessControl_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < ProcessManager.instance().Count; ++i)
            {
                float loc = ((float)ProcessManager.instance().At(i).Location) / ProcessManager.instance().MemorySize;
                float len = ((float)ProcessManager.instance().At(i).Length) / ProcessManager.instance().MemorySize;

                loc *= this.Width;
                len *= this.Width;
                e.Graphics.FillRectangle(new SolidBrush(ProcessManager.instance().At(i).Color), loc, 0.0f, len, (float)this.Height);
            }
        }

        private void ProcessControl_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
