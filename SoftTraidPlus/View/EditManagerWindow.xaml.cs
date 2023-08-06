using SoftTradePlus.Model.Data;
using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.View
{
    public partial class EditManagerWindow : Window
    {
        public EditManagerWindow(Manager selectedManager, string selectedName)
        {
            InitializeComponent();
            DataContext = new RegistrationWindowViewModel();
            RegistrationWindowViewModel.SelectedManagerName = selectedName;
            RegistrationWindowViewModel.SelectedManager = selectedManager;
        }
    }
}
