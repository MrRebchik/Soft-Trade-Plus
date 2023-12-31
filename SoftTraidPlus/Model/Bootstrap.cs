﻿using SoftTradePlus.View;
using SoftTradePlus.View.RegistrationWindow;
using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.Model
{
    public class Bootstrap
    {
        private SettingsWrapper _settingsWrapper;
        public Window Run()
        {
            _settingsWrapper = new SettingsWrapper();

            _settingsWrapper.Initialize();

            Window _window;

            if (!_settingsWrapper.IsAuthorized)
            {
                _window = new RegistrationWindow();

                _window.Show();
            }
            else
            {
                var _mainWindowViewModel = new MainWindowViewModel(_settingsWrapper.CurrentUser); //!!

                _window = new MainWindow(_mainWindowViewModel);

                _window.Show();
            }

            return _window;
        }
    }
}
