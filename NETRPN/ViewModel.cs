using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NETRPN
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly StringBuilder _x = new StringBuilder();
        private readonly Stack<double> _stack = new Stack<double>();

        public double X
        {
            get { return double.Parse(_x.ToString()); }
        }
        public double Y
        {
            get
            {
                return _stack.ElementAtOrDefault(0);
            }
        }
        public double Z
        {
            get
            {
                return _stack.ElementAtOrDefault(1);
            }
        }
        public double T
        {
            get
            {
                return _stack.ElementAtOrDefault(2);
            }
        }

        public string Xs => _x.ToString();
        public string Ys => Y.ToString();
        public string Zs => Z.ToString();
        public string Ts => T.ToString();

        public event PropertyChangedEventHandler PropertyChanged;

        public void PushVal(double newVal)
        {
            _stack.Push(newVal);
            RefreshAll();
        }

        public void PushX()
        {
            _stack.Push(X);
            _x.Clear();
            RefreshX();
        }

        public void ClearX()
        {
            _x.Clear();
            RefreshX();
        }


        public void ClearAll()
        {
            ClearX();
            _stack.Clear();
            RefreshAll();
        }

        public void Append(string val)
        {
            _x.Append(val);
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
