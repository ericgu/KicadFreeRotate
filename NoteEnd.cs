using System;
using System.Collections.Generic;
using System.IO;

namespace KiParser
{
    public class NodeEnd: Node
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

        public override void UpdateBounds(Bounds bounds)
        {
            if (X < bounds.XMin)
            {
                bounds.XMin = X;
            }
            if (Y < bounds.YMin)
            {
                bounds.YMin = Y;
            }

            if (X > bounds.XMax)
            {
                bounds.XMax = X;
            }
            if (Y > bounds.YMax)
            {
                bounds.YMax = Y;
            }
        }
    }
}