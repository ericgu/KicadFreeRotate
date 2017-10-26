using System;
using System.Collections.Generic;
using System.IO;

namespace KiParser
{
    public class Node
    {
        public string Name { get; set; }
        public virtual string Text { get; set; }

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

            Text = "";

            input.Next(); // Skip opening '('

            ParseName(input);
            //Console.WriteLine("Name: " + Name);

            while (Char.IsWhiteSpace(input.CurrentChar))
            {
                input.Next(); // skip spac
            }

            ParseBody(input);
        }

        private void ParseBody(Input input)
        {
            string text = String.Empty;

            bool inQuotes = false;
            while (!input.EndOfContent)
            {
                if (inQuotes)
                {
                    text += input.CurrentChar;

                    if (input.CurrentChar == '"')
                    {
                        inQuotes = false;
                    }
                    input.Next();
                }
                else
                {
                    if (input.CurrentChar == '(')
                    {
                        if (text.Length != 0)
                        {
                            NodeText nodeText = new NodeText();
                            nodeText.Text = text;
                            Children.Add(nodeText);
                            text = String.Empty;
                        }
                        Children.Add(CreateNode(input, this));
                    }
                    else if (input.CurrentChar == ')')
                    {
                        input.Next();
                        if (text.Length != 0)
                        {
                            NodeText nodeText = new NodeText();
                            nodeText.Text = text;
                            Children.Add(nodeText);
                            text = String.Empty;
                        }
                        CleanupText();

                        return;
                    }
                    else
                    {
                        if (input.CurrentChar == '"')
                        {
                            inQuotes = true;
                        }

                        text += input.CurrentChar;
                        input.Next();
                    }
                }
            }

            FinishParse();
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
                if (!Char.IsWhiteSpace(input.CurrentChar))
                {
                    Name += input.CurrentChar;
                }
                input.Next();
            }
        }

        private void CleanupText()
        {
            string cleaned = String.Empty;

            char lastChar = 'x';

            foreach (char s in Text)
            {
                if (s != '\n' && 
                    s != '\r' && 
                    (s != ' ' || lastChar != ' '))
                {
                    cleaned += s;
                    lastChar = s;
                }
            }

            Text = cleaned;
        }

        public override string ToString()
        {
            return Name + "[" + Children.Count + "]";
        }

        public void Save(StreamWriter writer, int indentLevel)
        {
            string indent = new String(' ', indentLevel * 2);

            string part = indent + "(" + Name + " " + Text;

            writer.Write(part);

            if (Children.Count == 0)
            {
                writer.WriteLine(")");
            }
            else
            {
                writer.WriteLine();
                foreach (Node child in Children)
                {
                    child.Save(writer, indentLevel + 1);
                }
                writer.WriteLine(indent + ")");
            }
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

        public virtual void FinishParse()
        { }
    }
}