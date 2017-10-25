using System;
using System.Collections.Generic;
using System.IO;

namespace KiParser
{
    public class NodeAt: Node
    {

        public double X
        {
            get
            {
                string[] parts = Text.Split(' ');
                return Double.Parse(parts[0]);
            }
        }

        public double Y
        {
            get
            {
                string[] parts = Text.Split(' ');
                return Double.Parse(parts[1]);

            }
        }
    }
}