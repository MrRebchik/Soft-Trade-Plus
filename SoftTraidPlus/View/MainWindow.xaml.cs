using System.Windows;
using System.Windows.Controls;
using SoftTradePlus.ViewModel;

namespace SoftTradePlus.View
{
    public partial class MainWindow : Window
    {
        public static ListView AllProducts;
        public static ListView AllClients;
        public MainWindow(IMainWindowViewModel MainWindowViewModel)
        {
            InitializeComponent();
            DataContext = MainWindowViewModel;
            AllProducts = ViewAllProducts;
            AllClients = ViewAllClients;
        }
    }
}
