using System.Collections.Generic;
using System.ComponentModel;
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
        public RPNMachine()
        {
           
        }

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
            for(var i = StackDepth-1; i > 0; i--)
            {
                _stack[i] = _stack[i - 1];
            }

            _stack[0] = val;
        }
        public Titem Pop()
        {
            var ret = _stack[0];

            for(var i = 0; i < _stack.Length - 1; i++)
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

        private string CleanedXString => new string(_xStringBuilder.ToString().ToCharArray().Where(k => char.IsDigit(k) || char.IsPunctuation(k)).ToArray());
        public Titem X
        {
            get
            {
                if(CleanedXString.Length == 0)
                {
                    return new Titem();
                }
                else
                {
                    return (Titem)TypeDescriptor.GetConverter(typeof(Titem)).ConvertFromString(CleanedXString);
                }
            }
        }
        public Titem Y => _stack.ElementAtOrDefault(StackPosY);
        public Titem Z => _stack.ElementAtOrDefault(StackPosZ);
        public Titem T => _stack.ElementAtOrDefault(StackPosT);

        public void SwapXY()
        {
            var valueY = _stack.ElementAtOrDefault(StackPosY).ToString();
            _stack[StackPosY] = X;
            _xStringBuilder.Clear();
            _xStringBuilder.Append(valueY);
        }
    }
}
