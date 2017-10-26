using System;

namespace KiParser
{
    public class Rotator
    {
        public Rotator(Bounds bounds, double rotationAngle)
        {
            RotationAngle = rotationAngle;
            Bounds = bounds;
        }

        public Bounds Bounds { get; set; }  

        public double RotationAngle { get; }
    }
}