using Windows.UI.Xaml.Controls;

namespace NETRPN
{
    public class TextBoxReg
    {
        public TextBox Box { get; set; }
        public string Name { get; set; }
        public string RegVal { set => Box.Text = $"{Name} = {value}"; }

    }
}
