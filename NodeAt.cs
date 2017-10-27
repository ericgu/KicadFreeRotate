using System;
using System.Linq;

namespace KiParser
{
    public class NodeAt: Node
    {
        public override void UpdateBounds(Bounds bounds)
        {
            bounds.UpdateBounds(V1, V2);
        }

        public void Rotate(Rotator rotator)
        {
            V3 = V3 + rotator.RotationAngle;
        }
    }
}