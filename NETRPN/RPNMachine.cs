using System.Linq;
using System.Text;

namespace NETRPN
{
    public class RPNMachine<Titem> where Titem : new()
    {
        private const int StackDepth = 3;
        private const int StackPosY = 0;
        private const int StackPosZ = 1;
        private const int StackPosT = 2;
        private readonly Titem[] _stack = new Titem[StackDepth];
        private readonly StringBuilder _xStringBuilder = new StringBuilder();
   
        public void AppendToX(string c)
        {
            _xStringBuilder.Append(c);
        }

        public void ClearX()
        {
            _xStringBuilder.Clear();
        }

        public void Push(Titem val)
        {
            for (var i = StackDepth - 1; i > 0; i--)
            {
                _stack[i] = _stack[i - 1];
            }

            _stack[0] = val;
        }
        public Titem Pop()
        {
            var ret = _stack[0];

            for (var i = 0; i < _stack.Length - 1; i++)
            {
                _stack[i] = _stack[i + 1];
            }

            _stack[StackDepth - 1] = new Titem();

            return ret;
        }

        public Titem this[int index]
        {
            get => _stack[index];
        }

        public void ClearAll()
        {
            Enumerable
                .Range(0, StackDepth)
                .ToList()
                .ForEach(i => _stack[i] = new Titem());
            ClearX();
        }


        public string Xs { 
            get => _xStringBuilder.ToString();
            set
            {
                _xStringBuilder.Clear();
                _xStringBuilder.Append(value);
            }
        }
        public Titem Y { 
            get => _stack.ElementAtOrDefault(StackPosY);
            set => _stack[StackPosY] = value;
        } 
        public Titem Z => _stack.ElementAtOrDefault(StackPosZ);
        public Titem T => _stack.ElementAtOrDefault(StackPosT);
    }
}
