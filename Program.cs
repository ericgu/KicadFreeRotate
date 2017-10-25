using System;
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

            string contents = File.ReadAllText(@"C:\KiParser\snowflake.kicad_pcb");

            Node node = Node.CreateNode(new Input(contents));

            using (StreamWriter writer = File.CreateText(@"C:\KiParser\snowflake_s.kicad_pcb"))
            {
                int indentLevel = 0;
                node.Save(writer, indentLevel);
            }
        }
    }
}
