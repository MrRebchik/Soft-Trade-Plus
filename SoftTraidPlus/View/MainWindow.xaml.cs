using System.Windows;
using SoftTradePlus.ViewModel;

namespace SoftTradePlus.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(IMainWindowViewModel MainWindowViewModel)
        {
            InitializeComponent();
        }
    }
}
