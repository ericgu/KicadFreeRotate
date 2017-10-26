using System;

namespace KiParser
{
    public class NodeAt: Node
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double R { get; set; }

        public override void FinishParse()
        {
            string[] parts = Text.Split(' ');
            X = Double.Parse(parts[2]);
            Y = Double.Parse(parts[2]);
            R = Double.Parse(parts[2]);
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

        public override string Text
        {
            get { return String.Format("{0} {1} {2}", X, Y, R); }
        }

    }
}