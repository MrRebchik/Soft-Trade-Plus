using SoftTradePlus.ViewModel;
using System.Windows;
namespace SoftTradePlus.View
{
    public partial class AddNewClientWindow : Window
    {
        public AddNewClientWindow(IMainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
