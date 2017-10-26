using System;

namespace KiParser
{
    public class NodeFpText: Node
    {
#if fred
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
#endif
    }
}