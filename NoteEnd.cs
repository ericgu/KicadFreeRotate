using System;
using System.Collections.Generic;
using System.IO;

namespace KiParser
{
    public class NodeEnd: Node
    {
        public override void UpdateBounds(Bounds bounds)
        {
            bounds.UpdateBounds(V1, V2);

        }
    }
}