using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETRPN
{
    public class ViewModel: INotifyPropertyChanged
    {
        private double _x;
        private Stack<double> _stack;
        public double X { get => _x; set {
                _x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }
        public double Y {
            get
            {
                return _stack.ElementAtOrDefault(0);
            }
        }
        public double Z {
            get
            {
                return _stack.ElementAtOrDefault(1);
            }
        }
        public double T {
            get
            {
                return _stack.ElementAtOrDefault(2);
            }
        }

        public string Xs => X.ToString();
        public string Ys => Y.ToString();
        public string Zs => Z.ToString();
        public string Ts => T.ToString();

        public event PropertyChangedEventHandler PropertyChanged;

        public void PushVal(double newVal)
        {
            _stack.Push(newVal);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Z)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(T)));
        }
    }
}
