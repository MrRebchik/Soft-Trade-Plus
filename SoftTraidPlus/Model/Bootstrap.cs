using SoftTradePlus.View.RegistrationWindow;
using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.Model
{
    public class Bootstrap
    {
        public Window Run()
        {
            var _registrationWindow = new RegistrationWindow(new RegistrationWindowViewModel());

            _registrationWindow.Show();

            return _registrationWindow;
        }
    }
}
