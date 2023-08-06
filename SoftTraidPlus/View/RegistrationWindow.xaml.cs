using System.Windows;
using System.Windows.Controls;
using SoftTradePlus.ViewModel;

namespace SoftTradePlus.View.RegistrationWindow
{
    public partial class RegistrationWindow : Window
    {
        public static ListView AllManagers;
        public RegistrationWindow() 
        {
            InitializeComponent();
            DataContext = new RegistrationWindowViewModel();
            AllManagers = ViewAllManagers;
        }
    }
}
