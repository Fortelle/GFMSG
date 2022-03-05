using System.Text;

namespace GFMSG
{
    public class SymbolQueue
    {
        private List<ISymbol> Queue { get; set; }
        private int Position { get; set; }

        public SymbolQueue(ISymbol[] queue)
        {
            Queue = new(queue);
            Position = 0;
        }

        public string Read(int count)
        {
            var sb = new StringBuilder();
            for(var i = 0; i < count; i++)
            {
                if(Queue[Position + i] is CharSymbol cs)
                {
                    sb.Append(cs.ToString());
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
            Position += count;
            return sb.ToString();
        }

        // todo
        public char? GetLastChar()
        {
            if (Position > 0 && Queue[Position - 1] is CharSymbol cs)
            {
                return cs.ToChar();
            }

            return null;
        }

        public bool ReadNext(out ISymbol? symbol)
        {
            if(Position < Queue.Count)
            {
                symbol = Queue[Position];
                Position++;
                return true;
            }
            else
            {
                symbol = null;
                return false;
            }
        }

        public void Insert(ISymbol symbol)
        {
            if(Position == Queue.Count)
            {
                Queue.Add(symbol);
            }
            else
            {
                Queue.Insert(Position + 1, symbol);
            }
        }

        public void Skip(int length)
        {
            Read(length);
        }
    }
}