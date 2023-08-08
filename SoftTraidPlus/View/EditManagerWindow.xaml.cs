using SoftTradePlus.Model.Data;
using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.View
{
    public partial class EditManagerWindow : Window
    {
        public EditManagerWindow(Manager selectedManager)
        {
            InitializeComponent();
            DataContext = new RegistrationWindowViewModel();
            RegistrationWindowViewModel.SelectedManagerName = selectedManager.Name;
            RegistrationWindowViewModel.SelectedManager = selectedManager;
        }
    }
}
