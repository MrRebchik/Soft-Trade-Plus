using System.Windows;
using SoftTradePlus.ViewModel;

namespace SoftTradePlus.View.RegistrationWindow
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow(IRegistrationWindowViewModel regWindowViewModel) // экземпляр окна нельзя создать без ViewModel
        {
            InitializeComponent();
        }
    }
}
