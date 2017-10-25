namespace KiParser
{
    public class Input
    {
        public Input(string contents)
        {
            Contents = contents;
            CurrentIndex = 0;
        }

        public string Contents { get; }
        public int CurrentIndex { get; set; }

        public bool EndOfContent { get { return CurrentIndex == Contents.Length; } }

        public void Next()
        {
            CurrentIndex++;
        }

        public char CurrentChar { get { return Contents[CurrentIndex]; } }

        public bool StartsWith(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != Contents[CurrentIndex + i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}