using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.View
{
    public partial class AddProductToCurrentClient : Window
    {
        public AddProductToCurrentClient(IMainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
