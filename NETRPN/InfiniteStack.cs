using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETRPN
{
    public class InfiniteStack<T>
    {
        private readonly Stack<T> _stack;
        public InfiniteStack()
        {
            _stack = new Stack<T>();
        }

        public void Push(T val)
        {
            _stack.Push(val);
        }
        public T Pop() => _stack.Count > 0 ? _stack.Pop() : default;

        public T this[int index]
        {
            get => _stack.ElementAtOrDefault(index);
        }

        public void Clear() => _stack.Clear();
    }
}
