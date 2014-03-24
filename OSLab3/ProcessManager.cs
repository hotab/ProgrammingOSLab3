using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OSLab3
{
    class ProcessManager
    {
        private List<Color> colors;
        private Random rand;
        private int memorySize;
        private int freeMemory;
        private List<MyProcess> listOfProcesses;
        
        private static ProcessManager _instance = null;
        private ProcessManager()
        {
            listOfProcesses = new List<MyProcess>();
            memorySize = 512;
            freeMemory = 512;
            colors = new List<Color>();
            colors.Add(Color.Lime);
            colors.Add(Color.Red);
            colors.Add(Color.Black);
            colors.Add(Color.Blue);
            colors.Add(Color.Brown);

            rand = new Random();
        }
        public static ProcessManager instance() { if (_instance == null) _instance = new ProcessManager(); return _instance; }
        
        public event EventHandler processesChanged;
        
        public int Count { get { return listOfProcesses.Count; } }
        public int MemorySize { get { return memorySize; } }
        public int FreeMemory { get { return freeMemory; } }
        public MyProcess At(int i)
        {
            return listOfProcesses[i];
        }

        public void CreateProcess(int size)
        {
            int i = 0;
            int segmentLen = 0;
            MyProcess proc = new MyProcess(size, 0);
            proc.Color = colors[rand.Next(colors.Count)];
            freeMemory -= size;
            if (listOfProcesses.Count > 0)
            {
                if (listOfProcesses[0].Location >= size)
                {
                    proc.Location = 0;
                    while (listOfProcesses[0].Color == proc.Color) proc.Color = colors[rand.Next(colors.Count)];
                    listOfProcesses.Insert(0, proc);
                    fireChanged();
                    return;
                }
            }
            for (i = 0; i < listOfProcesses.Count - 1; ++i)
            {
                segmentLen = listOfProcesses[i + 1].Location - listOfProcesses[i].Location - listOfProcesses[i].Length;
                if (segmentLen >= size)
                {
                    proc.Location = listOfProcesses[i].Location + listOfProcesses[i].Length;
                    while (listOfProcesses[i].Color == proc.Color || listOfProcesses[i + 1].Color == proc.Color) proc.Color = colors[rand.Next(colors.Count)];
                    listOfProcesses.Insert(i + 1, proc);
                    fireChanged();
                    return;
                }
            }
            if (listOfProcesses.Count == 0)
            {
                proc.Location = 0;
                listOfProcesses.Add(proc);
                fireChanged();
                return;
            }
            else
            {
                segmentLen = memorySize - listOfProcesses[i].Location - listOfProcesses[i].Length;
                if (segmentLen >= size)
                {
                    proc.Location = listOfProcesses[i].Location + listOfProcesses[i].Length;
                    while (listOfProcesses[i].Color == proc.Color) proc.Color = colors[rand.Next(colors.Count)];
                    listOfProcesses.Add(proc);
                    fireChanged();
                    return;
                }
            }

            freeMemory += size;
            System.Windows.Forms.MessageBox.Show("Cannot add process of size: " + size.ToString());
        }
        public void DeleteProcess(int num)
        {
            freeMemory += listOfProcesses[num].Length;
            listOfProcesses.RemoveAt(num);
            fireChanged();
        }
        public void Defragment()
        {
            if (listOfProcesses.Count > 0)
            {
                listOfProcesses[0].Location = 0;
                for (int i = 1; i < listOfProcesses.Count; ++i)
                {
                    listOfProcesses[i].Location = listOfProcesses[i - 1].Location + listOfProcesses[i - 1].Length;
                }
            }

            for (int i = 1; i < listOfProcesses.Count - 1; ++i)
            {
                while (listOfProcesses[i-1].Color == listOfProcesses[i].Color
                    || listOfProcesses[i+1].Color == listOfProcesses[i].Color)
                    listOfProcesses[i].Color = colors[rand.Next(colors.Count)];
            }
            fireChanged();
        }

        private void fireChanged()
        {
            if (processesChanged != null) processesChanged.Invoke(this, new EventArgs());
        }

        
    }
}
