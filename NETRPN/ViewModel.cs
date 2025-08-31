using System.ComponentModel;
using System.Linq;

namespace NETRPN
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly RPNMachine<double> _stack = new RPNMachine<double>();

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

        private string CleanedXString => new string(_stack.Xs.ToCharArray().Where(k => char.IsDigit(k) || char.IsPunctuation(k)).ToArray());
        public double X
        {
            get
            {
                if (CleanedXString.Length == 0)
                {
                    return new double();
                }
                else
                {
                    return (double)TypeDescriptor.GetConverter(typeof(double)).ConvertFromString(CleanedXString);
                }
            }
        }

        public double Y => _stack.Y;
        public double Z => _stack.Z;
        public double T => _stack.T;

        public string Xs => $"X = {X}";
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
            _stack.ClearX();
            RefreshAll();
        }

        public void ClearX()
        {
            _stack.ClearX();
            RefreshX();
            DotEnable = true;
        }

        public void ClearAll()
        {
            _stack.ClearAll();
            RefreshAll();
            DotEnable = true;
        }

        public void Append(string val)
        {
            _stack.AppendToX(val);
            if (val == ".")
                DotEnable = false;
            RefreshX();
        }

        public void SwapXY()
        {
            var x = X;
            var y = _stack.Y;
            _stack.Xs = y.ToString();
            _stack.Y = x;
            RefreshAll();
        }

        private void RefreshX() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Xs)));
        private void RefreshAll()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Xs)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ys)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Zs)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ts)));
        }
    }
}
