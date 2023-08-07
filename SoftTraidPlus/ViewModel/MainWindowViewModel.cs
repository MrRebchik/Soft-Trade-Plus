using SoftTradePlus.Model;
using SoftTradePlus.Model.Data;
using SoftTradePlus.View;
using SoftTradePlus.View.RegistrationWindow;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SoftTradePlus.ViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        public MainWindowViewModel(Manager manager)
        {
            _currentManager = manager;
        }

        #region Properties
        private Manager _currentManager;
        public Manager CurrentManager { get; set; }
        
        public string ManagerName 
        { 
            get
            {
                return "Текущий пользователь: " + _currentManager.Name;
            } 
        }

        private List<Client> _allClients = SQLHelper.GetAllClients();
        public List<Client> AllClients
        {
            get { return _allClients.Where(item => item.ManagerId == _currentManager.Id).ToList(); }
            set 
            {
                _allClients = value;
                NotifyPropertyChanged(nameof(AllClients));
            }
        }

        private List<Product> _allProducts = SQLHelper.GetAllProducts();
        public List<Product> AllProducts
        {
            get { return _allProducts; }
            set
            {
                _allProducts = value;
                NotifyPropertyChanged(nameof(AllProducts));
            }
        }
        
        private List<Transaction> _allTransactions = SQLHelper.GetAllTransactions();
        public List<Transaction> AllTransactions
        {
            get { return _allTransactions; }
            set
            {
                _allTransactions = value;
                NotifyPropertyChanged(nameof(AllTransactions));
            }
        }

        private static Client _currentClient;
        public static Client CurrentClient
        {
            get
            {
                return _currentClient;
            }
            set
            {
                _currentClient = value;
            }
        }

        private static Product _currentProduct;
        public static Product CurrentProduct
        {
            get
            {
                return _currentProduct;
            }
            set
            {
                _currentProduct = value;
            }
        }

        // свойства для продуктов
        public class ProductTypeClass
        {
            public ProductTypeClass(string name, int num)
            {
                Name = name;
                Number = num;
            }
            public string Name { get; set; }
            public int Number { get; set; }
        }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }

        private List<ProductTypeClass> _allProductTypes = new List<ProductTypeClass>()
        {
            new ProductTypeClass("Лицензия",0),
            new ProductTypeClass("Временная подписка",1)
        };
        public List<ProductTypeClass> AllProductTypes 
        {
            get
            {
                return _allProductTypes;
            }
            set
            {
                _allProductTypes = value;
            }
        }
        public ProductTypeClass ProductType { get; set; }


        // свойства для клиентов

        public class ClientStatusClass
        {
            public ClientStatusClass(string name, int num)
            {
                Name = name;
                Number = num;
            }
            public string Name { get; set; }
            public int Number { get; set; }
        }
        public string ClientName { get; set; }

        private List<ClientStatusClass> _allClientStatuses = new List<ClientStatusClass>()
        {
            new ClientStatusClass("Обычный",0),
            new ClientStatusClass("Ключевой",1)
        };
        public List<ClientStatusClass> AllClientStatuses
        {
            get
            {
                return _allClientStatuses;
            }
            set
            {
                _allClientStatuses = value;
            }
        }
        public ClientStatusClass ClientStatus { get; set; }

        #endregion

        #region Commands
        private RelayCommand _opeAddNewProductWindow;
        public RelayCommand OpenAddNewProductWindow
        {
            get
            {
                return _opeAddNewProductWindow ?? new RelayCommand(obj =>
                {
                    OpenAddNewProductWindowMethod();
                }
                );
            }
        }
        private RelayCommand _addNewProduct;
        public RelayCommand AddNewProduct
        {
            get
            {
                return _addNewProduct ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (ProductName != null && ProductPrice > 0 && ProductType != null)
                    {
                        SQLHelper.CreateProduct(ProductName, ProductPrice, (Model.Data.ProductType)ProductType.Number);
                        wnd.Close();
                        UpdateAllProductsView();
                    }
                }
                );
            }
        }

        private RelayCommand _openAddNewClientWindow;
        public RelayCommand OpenAddNewClientWindow
        {
            get
            {
                return _openAddNewClientWindow ?? new RelayCommand(obj =>
                {
                    OpenAddNewClientWindowMethod();
                }
                );
            }
        }

        private RelayCommand _addNewClient;
        public RelayCommand AddNewClient
        {
            get
            {
                return _addNewClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (ClientName != null && ClientName.Replace(" ","").Length != 0 && ClientStatus != null)
                    {
                        SQLHelper.CreateClient(ClientName, _currentManager.Id, (Model.Data.ClientStatus)ClientStatus.Number);
                        wnd.Close();
                        UpdateAllClientsView();
                    }
                }
                );
            }
        }
        #endregion

        #region Update View
        private void UpdateAllProductsView()
        {
            AllProducts = SQLHelper.GetAllProducts();
            MainWindow.AllProducts.ItemsSource = null;
            MainWindow.AllProducts.Items.Clear();
            MainWindow.AllProducts.ItemsSource = AllProducts;
            MainWindow.AllProducts.Items.Refresh();
        }
        private void UpdateAllClientsView()
        {
            AllClients = SQLHelper.GetAllClients();
            MainWindow.AllClients.ItemsSource = null;
            MainWindow.AllClients.Items.Clear();
            MainWindow.AllClients.ItemsSource = AllClients;
            MainWindow.AllClients.Items.Refresh();
        }
        #endregion

        #region Open Windows
        private void OpenAddNewProductWindowMethod()
        {
            AddNewProductWindow addNewProductWindow = new AddNewProductWindow(this);
            SetCenterPositionAndOpen(addNewProductWindow);
        }
        private void OpenAddNewClientWindowMethod()
        {
            AddNewClientWindow addNewClientWindow = new AddNewClientWindow(this);
            SetCenterPositionAndOpen(addNewClientWindow);
        }
        private void OpenEditProductWindowMethod()
        {
            //EditProductWindow editProductWindow = new EditProductWindow(selectedProduct,);
        }
        private void SetCenterPositionAndOpen(Window wnd)
        {
            wnd.Owner = Application.Current.MainWindow;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.ShowDialog();
        }
        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
