using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KiParser
{
    public class Node
    {
        public string Name { get; set; }

        public List<Node> Children { get; private set; }

        public Node Parent { get; private set; }

        public Node()
        {
            Children = new List<Node>();
        }

        public void Parse(Input input)
        {
            if (input.CurrentChar != '(')
            {
                throw new Exception("Node must start with '('");
            }

            input.Next(); // Skip opening '('

            ParseName(input);
            //Console.WriteLine("Name: " + Name);

            ParseBody(input);
        }

        private void ParseBody(Input input)
        {
            while (!input.EndOfContent)
            {
                string text = ParseUntilDelimiter(input, '(', ')');
                NodeText nodeText = new NodeText();
                nodeText.Text = text;
                Children.Add(nodeText);

                if (input.CurrentChar == '(')
                {
                    Children.Add(CreateNode(input, this));
                }
                else if (input.CurrentChar == ')')
                {
                    input.Next();

                    return;
                }
            }
        }

        private string ParseUntilDelimiter(Input input, params char[] delimiters)
        {
            string text = String.Empty;

            bool inQuotes = false;
            while (!input.EndOfContent)
            {
                if (!inQuotes)
                {
                    foreach (char delimiter in delimiters)
                    {
                        if (input.CurrentChar == delimiter)
                        {
                            return text;
                        }
                    }
                }

                if (input.CurrentChar == '"')
                {
                    inQuotes = !inQuotes;
                }

                text += input.CurrentChar;
                input.Next();
            }

            return text;
        }

        public static Node CreateNode(Input input, Node parent)
        {
            Node node;
            if (input.StartsWith("(at"))
            {
                node = new NodeAt();
            }
            else if (input.StartsWith("(start"))
            {
                node = new NodeStart();
            }
            else if (input.StartsWith("(end"))
            {
                node = new NodeEnd();
            }
            else if (input.StartsWith("(fp_text"))
            {
                node = new NodeFpText();
            }
            else if (input.StartsWith("(fp_line"))
            {
                node = new NodeFpLine();
            }
            else if (input.StartsWith("(pad"))
            {
                node = new NodePad();
            }
            else
            {
                node = new Node();
            }

            node.Parse(input);
            node.Parent = parent;

            return node;
        }

        private void ParseName(Input input)
        {
                // extract and save node name
            while (!input.EndOfContent && input.CurrentChar != ' ')
            {
                Name += input.CurrentChar;
                input.Next();
            }
        }


        public override string ToString()
        {
            return Name + "[" + Children.Count + "]";
        }

        public virtual string NodeStartText
        {
            get { return "(" + Name; }
        }

        public virtual string NodeEndText
        {
            get { return ")"; }
        }

        private NodeText TextChild
        {
            get { return (NodeText) Children.First(); }
        }

        public double V1
        {
            get { return TextChild.GetTextPartAsDouble(1); }
            set { TextChild.SetTextPartAsDouble(1, value); }
        }

        public double V2
        {
            get { return TextChild.GetTextPartAsDouble(2); }
            set { TextChild.SetTextPartAsDouble(2, value); }
        }

        public double V3
        {
            get { return TextChild.GetTextPartAsDouble(3); }
            set { TextChild.SetTextPartAsDouble(3, value); }
        }

        public void Save(StreamWriter writer, int indentLevel)
        {
            writer.Write(NodeStartText);

            foreach (Node child in Children)
            {
                child.Save(writer, indentLevel + 1);
            }
            writer.Write(NodeEndText);

        }

        public void TraverseAll<T>(Action<Node, T> process, Func<Node, bool> selector, T userData)
        {
            if (selector(this))
            {
                process(this, userData);

                foreach (Node child in Children)
                {
                    child.TraverseAll(process, selector, userData);
                }
            }
        }

        public virtual void UpdateBounds(Bounds bounds)
        {
        }
    }
}