using SoftTradePlus.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SoftTradePlus.View
{
    public partial class CurrentClientsProducts : Window
    {
        public static ListView AllTransactions;
        public CurrentClientsProducts(IMainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            AllTransactions = ViewAllTransactions;
        }
    }
}
