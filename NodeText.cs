using System;
using System.Collections.Generic;
using System.Linq;

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
            var parts = Text.Split(' ');
            if (parts.Length - 1 < index)
            {
                return null;
            }

            return parts[index];
        }

        public void SetTextPart(int index, string value)
        {
            var parts = Text.Split(' ').ToList();

            while (parts.Count < index + 1)
            {
                parts.Add("");
            }
            parts[index] = value;
            Text = String.Join(" ", parts);
        }

        public double GetTextPartAsDouble(int index)
        {
            var textPart = GetTextPart(index);
            return textPart != null ? Double.Parse(textPart) : 0.0;
        }

        public void SetTextPartAsDouble(int index, double value)
        {
            SetTextPart(index, value.ToString());
        }
    }
}