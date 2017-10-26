using System;
using System.Collections.Generic;

namespace KiParser
{
    public class NodeText: Node
    {
        public override string ToString()
        {
            return Text;
        }

    }
}