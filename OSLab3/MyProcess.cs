using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSLab3
{
    class MyProcess
    {
        public int Length { get; private set; }
        public int Location { get; set; }
        public System.Drawing.Color Color { get; set; }
        public MyProcess(int length, int location) 
        {
            Length = length;
            Location = location;
        }
    }
}
