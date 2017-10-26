﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiParser
{
    class Program
    {
        static void Main(string[] args)
        {
#if home
            var inputFile = @"D:\data\Electronics\Snowflake\broken\upper\snowflake.kicad_pcb";
            var outputFile = @"D:\data\Electronics\Snowflake\broken\upper\snowflake_s.kicad_pcb";
#else
            var inputFile = @"c:\kiparser\snowflake.kicad_pcb";
            var outputFile = @"c:\kiparser\snowflake_s.kicad_pcb";
#endif

            string contents = File.ReadAllText(inputFile);

            Node node = Node.CreateNode(new Input(contents), null);

            var bounds = Bounds.GetBounds(node);
            double x = bounds.XMax;

            Rotator rotator = new Rotator(bounds, 38);

            node.TraverseAll(RotateNode, SelectNode, rotator);

            using (StreamWriter writer = File.CreateText(outputFile))
            {
                int indentLevel = 0;
                node.Save(writer, indentLevel);
            }
        }

        private static bool SelectNode(Node node)
        {
            return true;
        }

        private static void RotateNode(Node node, Rotator rotator)
        {
            NodeAt nodeAt = node as NodeAt;

            if (nodeAt != null)
            {
                nodeAt.V3 = nodeAt.V3 + rotator.RotationAngle;
            }
        }
    }
}
