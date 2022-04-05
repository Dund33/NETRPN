using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NETRPN
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Stack<double> _stack = new Stack<double>();
        private StringBuilder _x = new StringBuilder();
        private bool _displayingRes = false;

        private TextBoxReg _yReg;
        private TextBoxReg _zReg;
        private TextBoxReg _tReg;

        private MathOps _ops = new MathOps();

        public ViewModel ViewModel { get; set; } = new ViewModel();

        public MainPage() 
        {
            this.InitializeComponent();
            _yReg = new TextBoxReg()
            {
                Box = yTextBox,
                Name = "Y"
            };
            _zReg = new TextBoxReg()
            {
                Box = zTextBox,
                Name = "Z"
            };
            _tReg = new TextBoxReg()
            {
                Box = tTextBox,
                Name = "T"
            };
            this.DataContext = X;
            RefreshGui();
        }


        private void DigitPressedEventHandler(object sender, RoutedEventArgs args)
        {
            var button = sender as Button;
            if (button.Content is string content)
            {
                if (_displayingRes)
                {
                    PushXOnStack();
                    _displayingRes = false;
                }
                X.Append(content);
            }
            RefreshGui();
        }

        private void FuncPressedEventHandler(object sender, RoutedEventArgs args)
        {
            var button = sender as Button;
            if (button.Content is string content)
            {
                if (content == "CLEAR X")
                {
                    X.Clear();
                    RefreshGui();
                    _displayingRes = false;
                }
                else if (content == "AC")
                {
                    X.Clear();
                    _stack.Clear();
                    RefreshGui();
                    _displayingRes = false;
                }
                else if (content == "ENTER")
                {
                    if (_displayingRes)
                        _displayingRes = false;
                    PushXOnStack();
                }
                else
                {
                    var arg1 = double.TryParse(X.X, out var tmp1) ? tmp1 : 0.0;
                    var arg2 = _stack.TryPop(out var tmp2) ? tmp2 as double? : null;
                    var res = _ops.Eval(arg1, arg2, content);
                    X.Clear();
                    X.Append(res.ToString());
                    _displayingRes = true;
                }
            }
            RefreshGui();
        }

        private void PushXOnStack()
        {
            var xRegVal = double.Parse(_x.ToString());
            _stack.Push(xRegVal);
            X.Clear();
        }

        private void RefreshGui()
        {
            //xTextBox.Text = _x.ToString();
            _yReg.RegVal = _stack.Count > 0 ? _stack.ElementAt(0).ToString() : "EMPTY";
            _zReg.RegVal = _stack.Count > 1 ? _stack.ElementAt(1).ToString() : "EMPTY";
            _tReg.RegVal = _stack.Count > 2 ? _stack.ElementAt(2).ToString() : "EMPTY";
        }
    }
}
