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
        private MathOps _ops = new MathOps();

        public ViewModel ViewModel { get; set; } = new ViewModel();

        public MainPage()
        {
            this.InitializeComponent();
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
                else
                {
                    var res = _ops.Eval(ViewModel.X, ViewModel.Y, content);
                    ViewModel.PopVal();
                    ViewModel.ClearX();
                    ViewModel.Append(res.ToString());
                    _displayingRes = true;
                }
            }
        }
    }
}
