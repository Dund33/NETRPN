using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NETRPN
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly StringBuilder _x = new StringBuilder();
        private readonly InfiniteStack<double> _stack = new InfiniteStack<double>();

        private bool _dotenable = true;
        public bool DotEnable
        {
            get => _dotenable;

            set
            {
                _dotenable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DotEnable)));
            }
        }

        public double X
        {
            get
            {
                var fixedString = new string(Xs.ToCharArray().Where(k => char.IsDigit(k) || char.IsPunctuation(k)).ToArray());
                var resultDouble = double.TryParse(fixedString, out var result) ? result : default;
                return resultDouble;
            }
            set
            {
                _x.Clear(); 
                _x.Append(value);
                RefreshX();
            }
        }
        public double Y
        {
            get
            {
                return _stack[0];
            }
            set
            {
                _stack.Pop();
                _stack.Push(value);
                RefreshY();
            }
        }
        public double Z
        {
            get
            {
                return _stack[1];
            }
        }
        public double T
        {
            get
            {
                return _stack[2];
            }
        }

        public string Xs => _x.Length > 0 ? $"X = {_x}" : $"X = {default(double)}";
        public string Ys => $"Y = {Y}";
        public string Zs => $"Z = {Z}";
        public string Ts => $"T = {T}";

        public event PropertyChangedEventHandler PropertyChanged;

        public void PushVal(double newVal)
        {
            _stack.Push(newVal);
            RefreshAll();
        }

        public void PopVal()
        {
            _stack.Pop();
            RefreshAll();
        }

        public void PushX()
        {
            _stack.Push(X);
            _x.Clear();
            RefreshAll();
        }

        public void ClearX()
        {
            _x.Clear();
            RefreshX();
            DotEnable = true;
        }


        public void ClearAll()
        {
            ClearX();
            _stack.Clear();
            RefreshAll();
            DotEnable = true;
        }

        public void Append(string val)
        {
            _x.Append(val);
            if (val == ".")
                DotEnable = false;
            RefreshX();
        }

        private void RefreshX() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Xs)));
        private void RefreshY() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ys)));
        private void RefreshAll()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Xs)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ys)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Zs)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ts)));
        }
    }
}
