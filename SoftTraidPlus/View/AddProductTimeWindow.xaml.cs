using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.View
{
    public partial class AddProductTimeWindow : Window
    {
        public AddProductTimeWindow(IMainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
