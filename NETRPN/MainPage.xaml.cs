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
        private bool _displayingRes = false;
        private readonly MathOps _ops = new MathOps();

        public ViewModel ViewModel { get; set; } = new ViewModel();

        public MainPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void DigitPressedEventHandler(object sender, RoutedEventArgs args)
        {
            var button = sender as Button;
            if (button.Content is string content)
            {
                if (_displayingRes)
                {
                    ViewModel.PushX();
                    _displayingRes = false;
                }
                ViewModel.Append(content);
            }
        }

        private void FuncPressedEventHandler(object sender, RoutedEventArgs args)
        {
            var button = sender as Button;
            if (button.Content is string content)
            {
                if (content == "CLEAR X")
                {
                    ViewModel.ClearX();
                    _displayingRes = false;
                }
                else if (content == "AC")
                {
                    ViewModel.ClearAll();
                    _displayingRes = false;
                }
                else if (content == "ENTER")
                {
                    _displayingRes = false;
                    ViewModel.PushX();
                }
                else if(content == "X<=>Y")
                {
                    var oldX = ViewModel.X;
                    ViewModel.X = ViewModel.Y;
                    ViewModel.Y = oldX;
                }
                else
                {
                    MathOps.Operation func = MathOps.Operation.Noop;

                    switch (content)
                    {
                        case "SIN":
                            func = MathOps.Operation.Sin;
                            break;
                        case "COS":
                            func = MathOps.Operation.Cos;
                            break;
                        case "TAN":
                            func = MathOps.Operation.Tan;
                            break;
                        case "SQRT":
                            func = MathOps.Operation.Sqrt;
                            break;
                        case "*":
                            func = MathOps.Operation.Mult;
                            break;
                        case "/":
                            func = MathOps.Operation.Div;
                            break;
                        case "+":
                            func = MathOps.Operation.Add;
                            break;
                        case "-":
                            func = MathOps.Operation.Sub;
                            break;
                        case "EXP":
                            func = MathOps.Operation.Exp;
                            break;
                        case "LOG":
                            func = MathOps.Operation.Log;
                            break;
                    }

                    var res = _ops.Eval(ViewModel.X, ViewModel.Y, func);
                    ViewModel.PopVal();
                    ViewModel.ClearX();
                    ViewModel.Append(res.ToString());
                    _displayingRes = true;
                }
            }
        }
    }
}
