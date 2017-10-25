using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiParser
{
    struct Point
    {
        public double X;
        public double Y;
    }

    class Program
    {
        static void Main(string[] args)
        {

            string contents = File.ReadAllText(@"C:\KiParser\snowflake.kicad_pcb");

            Node node = Node.CreateNode(new Input(contents));

            min.X = Double.MaxValue;
            min.Y = Double.MaxValue;
            max.X = -Double.MaxValue;
            max.Y = -Double.MaxValue;
            node.TraverseAll(Process);

            using (StreamWriter writer = File.CreateText(@"C:\KiParser\snowflake_s.kicad_pcb"))
            {
                int indentLevel = 0;
                node.Save(writer, indentLevel);
            }
        }

        static private Point min;
        static private Point max;

        static void Process(Node node)
        {
            NodeAt nodeAt = node as NodeAt;

            if (nodeAt != null)
            {
                if (nodeAt.X < min.X)
                {
                    min.X = nodeAt.X;
                }
                if (nodeAt.Y < min.Y)
                {
                    min.Y = nodeAt.Y;
                }

                if (nodeAt.X > max.X)
                {
                    max.X = nodeAt.X;
                }
                if (nodeAt.Y > max.Y)
                {
                    max.Y = nodeAt.Y;
                }
            }
        }
    }
}
