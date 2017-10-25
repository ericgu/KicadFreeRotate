using System;
using System.Collections.Generic;
using System.IO;

namespace KiParser
{
    public class Node
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public List<Node> Children { get; private set; }

        public void Parse(Input input)
        {
            if (input.CurrentChar != '(')
            {
                throw new Exception("Node must start with '('");
            }

            Children = new List<Node>();
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
            bool inQuotes = false;
            while (!input.EndOfContent)
            {
                if (inQuotes)
                {
                    Text += input.CurrentChar;

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
                        Children.Add(CreateNode(input));
                    }
                    else if (input.CurrentChar == ')')
                    {
                        input.Next();
                        CleanupText();
                        //Console.WriteLine("Text: " + Text);
                        return;
                    }
                    else
                    {
                        if (input.CurrentChar == '"')
                        {
                            inQuotes = true;
                        }

                        Text += input.CurrentChar;
                        input.Next();
                    }
                }
            }

            Console.WriteLine("Text: " + Text);
        }

        public static Node CreateNode(Input input)
        {
            Node node;
            if (input.StartsWith("(at"))
            {
                node = new NodeAt();
            }
            else if (input.StartsWith("(start"))
            {
                node = new NodeAt();
            }
            else if (input.StartsWith("(start"))
            {
                node = new NodeAt();
            }
            else
            {
                node = new Node();
            }

            node.Parse(input);

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
    }
}