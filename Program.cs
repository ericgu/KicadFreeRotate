using System.Collections.Generic;
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

            string contents = File.ReadAllText(@"D:\data\Electronics\Snowflake\broken\upper\snowflake.kicad_pcb");

            Node node = Node.CreateNode(new Input(contents), null);

            var bounds = Bounds.GetBounds(node);
            double x = bounds.XMax;

            Rotator rotator = new Rotator(bounds, 38);

            node.TraverseAll(RotateNode, SelectNode, rotator);

            using (StreamWriter writer = File.CreateText(@"D:\data\Electronics\Snowflake\broken\upper\snowflake_s.kicad_pcb"))
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
                nodeAt.R = nodeAt.R + rotator.RotationAngle;
            }
        }
    }
}
