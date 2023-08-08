using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.View
{
    public partial class AddNewProductWindow : Window
    {
        public AddNewProductWindow(IMainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
