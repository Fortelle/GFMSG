namespace GFMSG
{

    public class TextSymbol : ISymbol
    {
        public string Text { get; set; }

        public int Size => Text.Length * 2;

        public TextSymbol(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }

    }

}
