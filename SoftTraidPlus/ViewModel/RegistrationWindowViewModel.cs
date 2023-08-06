using SoftTradePlus.Model;
using SoftTradePlus.View;
using SoftTradePlus.Model.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System;
using SoftTradePlus.View.RegistrationWindow;

namespace SoftTradePlus.ViewModel
{
    public class RegistrationWindowViewModel : IRegistrationWindowViewModel, INotifyPropertyChanged
    {
        private List<Manager> _allManagers = SQLHelper.GetAllManagers();
        public List<Manager> AllManagers 
        { 
            get
            {
                return _allManagers;
            } 
            set 
            { 
                _allManagers = value; 
                NotifyPropertyChanged(nameof(AllManagers));
            }
        }

        private static Manager _selectedManager;
        public static Manager SelectedManager 
        {
            get
            { 
                return _selectedManager; 
            }
            set
            { 
                _selectedManager = value;
            }
        }

        private bool _storeSelection;
        public bool StoreSelection 
        {
            get 
            {
                return _storeSelection;
            }
            set
            {
                _storeSelection = value;
                NotifyPropertyChanged(nameof(StoreSelection));
            }
        }
        public string ManagerName { get; set; }
        public static string SelectedManagerName { get; set; }

        #region Commands

        private RelayCommand _openAddNewManagerWindow;
        public RelayCommand OpenAddNewManagerWindow
        {
            get 
            {
                return _openAddNewManagerWindow ?? new RelayCommand(obj =>
                {
                    OpenAddNewManagerWindowMethod();
                }
                );

            }
        }

        private RelayCommand _addNewManager;
        public RelayCommand AddNewManager
        {
            get
            {
                return _addNewManager ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if(ManagerName  == null || ManagerName.Replace(" ","").Length == 0)
                    {
                        throw new Exception("Некорректное имя");
                    }
                    else
                    {
                        SQLHelper.CreateManager(ManagerName);
                        wnd.Close();
                        UpdateAllManagersView();
                    }
                }
                );

            }
        }

        private RelayCommand _deleteManager;
        public RelayCommand DeleteManager
        {
            get
            {
                return _deleteManager ?? new RelayCommand(obj => 
                {
                    if (SelectedManager != null)
                    {
                        SQLHelper.DeleteManager(SelectedManager);
                        SelectedManager = null;
                        UpdateAllManagersView();
                    }
                }
                );
                
            }
        }

        private RelayCommand _openEditManagerWindow;
        public RelayCommand OpenEditManager
        {
            get
            {
                return _openEditManagerWindow ?? new RelayCommand(obj =>
                {
                    if (SelectedManager != null)
                    {
                        OpenEditManagerWindowMethod(SelectedManager, SelectedManager.Name);
                    }
                }
                );

            }
        }

        private RelayCommand _editManager;
        public RelayCommand EditManager
        {
            get
            {
                return _editManager ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (SelectedManagerName == null || SelectedManagerName.Replace(" ", "").Length == 0)
                    {
                        throw new Exception("Некорректное имя");
                    }
                    else
                    {
                        SQLHelper.EditManager(SelectedManager,SelectedManagerName);
                        wnd.Close();
                        UpdateAllManagersView();
                    }
                }
                );
            }
        }

        private RelayCommand _openMainWindow;
        public RelayCommand OpenMainWindow
        {
            get
            {
                return _openMainWindow ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (SelectedManager != null)
                    {
                        MainWindow mainWindow = new MainWindow(new MainWindowViewModel(_selectedManager));
                        mainWindow.Show();
                        wnd.Close();
                    }
                }
                );

            }
        }
        #endregion

        #region View Update
        private void UpdateAllManagersView()
        {
            AllManagers = SQLHelper.GetAllManagers();
            RegistrationWindow.AllManagers.ItemsSource = null;
            RegistrationWindow.AllManagers.Items.Clear();
            RegistrationWindow.AllManagers.ItemsSource = AllManagers;
            RegistrationWindow.AllManagers.Items.Refresh();
        }
        #endregion

        #region OpenWindows
        private void OpenEditManagerWindowMethod(Manager selectedManager, string name)
        {
            EditManagerWindow editManagerWindow = new EditManagerWindow(selectedManager, name);
            SetCenterPositionAndOpen(editManagerWindow);
        }

        private void OpenAddNewManagerWindowMethod()
        {
            AddNewManagerWindow newManagerWindow = new AddNewManagerWindow();
            SetCenterPositionAndOpen(newManagerWindow);
        }
        #endregion
        private void SetCenterPositionAndOpen(Window wnd)
        {
            wnd.Owner = Application.Current.MainWindow;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.ShowDialog();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
