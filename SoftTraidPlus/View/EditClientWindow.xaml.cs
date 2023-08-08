using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.View
{
    public partial class EditClientWindow : Window
    {
        public EditClientWindow(IMainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
