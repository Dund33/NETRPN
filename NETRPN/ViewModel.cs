using System.ComponentModel;
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
            get { return double.Parse(Xs); }
        }
        public double Y
        {
            get
            {
                return _stack[0];
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

        public string Xs => _x.Length > 0 ? _x.ToString() : default(double).ToString();
        public string Ys => Y.ToString();
        public string Zs => Z.ToString();
        public string Ts => T.ToString();

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
        private void RefreshAll()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Xs)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ys)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Zs)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ts)));
        }
    }
}
