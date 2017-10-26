using System;

namespace KiParser
{
    public class Bounds
    {
        public Bounds()
        {
            XMin = Double.MaxValue;
            YMin = Double.MaxValue;
            XMax = -Double.MaxValue;
            YMax = -Double.MaxValue;
        }

        public double XMin;
        public double YMin;
        public double XMax;
        public double YMax;

        public static void UpdateBounds(Bounds bounds, Node node)
        {
            node.UpdateBounds(bounds);
        }

        public double XCenter
        {
            get { return (XMin + XMax) / 2; }
        }

        public double YCenter
        {
            get { return (YMin + YMax) / 2; }
        }

        public static Bounds GetBounds(Node node)
        {
            var bounds = new Bounds();
            node.TraverseAll(Process, Selector, bounds);
            return bounds;
        }

        private static bool Selector(Node node)
        {
            if (node is NodeFpLine || node is NodeFpText || node is NodePad)
            {
                return false;
            }

            return true;
        }

        private static void Process(Node node, Bounds bounds)
        {
            Bounds.UpdateBounds(bounds, node);
        }

        public void UpdateBounds(double X, double Y)
        {
            if (X < XMin)
            {
                XMin = X;
            }
            else if (Y < YMin)
            {
                YMin = Y;
            }

            if (X > XMax)
            {
                XMax = X;
            }
            else if (Y > YMax)
            {
                YMax = Y;
            }
        }
    }
}