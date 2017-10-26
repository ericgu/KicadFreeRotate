using System;
using System.Collections.Generic;

namespace KiParser
{
    public class NodeText: Node
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public override string NodeStartText
        {
            get { return Text; }
        }

        public override string NodeEndText
        {
            get { return ""; }
        }

        public string GetTextPart(int index)
        {
            return Text.Split(' ')[index];
        }

        public void SetTextPart(int index, string value)
        {
            var parts = Text.Split(' ');
            parts[index] = value;
            Text = String.Join(" ", parts);
        }

        public double GetTextPartAsDouble(int index)
        {
            return Double.Parse(GetTextPart(index));
        }

        public void SetTextPartAsDouble(int index, double value)
        {
            SetTextPart(index, value.ToString());
        }
    }
}