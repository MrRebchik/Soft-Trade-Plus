using SoftTradePlus.Model.Data;

namespace SoftTradePlus.ViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private Manager _currentClient;
        public MainWindowViewModel(Manager client)
        { 
            _currentClient = client;
        }
    }
}
