using System;

namespace KiParser
{
    public class NodeFpLine: Node
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