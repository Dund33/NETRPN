using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NETRPN
{
    public class ObservableStringBuilder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private StringBuilder _value = new StringBuilder();
        public string X { get => _value.ToString();  set{
                _value = new StringBuilder(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }

        public void Append(string x)
        {
            _value.Append(x);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
        }

        public void Clear()
        {
            _value.Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
        }

    }
}
