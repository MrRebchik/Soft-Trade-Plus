﻿using SoftTradePlus.ViewModel;
using System.Windows;

namespace SoftTradePlus.View
{
    public partial class AddNewManagerWindow : Window
    {
        public AddNewManagerWindow()
        {
            InitializeComponent();
            DataContext = new RegistrationWindowViewModel();
        }
    }
}
