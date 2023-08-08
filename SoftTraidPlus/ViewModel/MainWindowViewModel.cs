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
        public Manager CurrentManager
        {
            get { return _currentManager; }
            set { _currentManager = value; }
        }

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
        //
        public string SelectedProductName
        {
            get { return CurrentProduct.Name; }
            set { CurrentProduct.Name = value; }
        }
        public float SelectedProductPrice
        {
            get { return (float)CurrentProduct.Price; }
            set { CurrentProduct.Price = value; }
        }
        public ProductTypeClass SelectedProductType
        {
            get
            {
                if (CurrentProduct.Type == Model.Data.ProductType.Limited)
                {
                    return _allProductTypes[1];
                }
                else return _allProductTypes[0];
            }
            set
            {
                CurrentProduct.Type = (ProductType)value.Number;
            }
        }


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
        public string ClientName
        {
            get { return CurrentClient.Name; }
            set { CurrentClient.Name = value; }
        }

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
        public ClientStatusClass ClientStatus
        {
            get
            {
                    if (CurrentClient.Status == Model.Data.ClientStatus.CrucialClient)
                    {
                        return _allClientStatuses[1];
                    }
                    else return _allClientStatuses[0];

            }
            set
            {
                CurrentClient.Status = (ClientStatus)value.Number;
            }
        }

        // свойства списка покупок

        private List<Transaction> _currentClientProducts;
        public List<Transaction> CurrentClientProducts
        {
            get
            {
                return AllTransactions.
                    Where(el => el.ClientId == CurrentClient.Id).
                    ToList();
            }
            set
            {
                _currentClientProducts = value;
            }
        }

        private Transaction _currentTransaction;
        public Transaction CurrentTransaction
        {
            get; set;
        }

        public class DurationClass
        {
            public DurationClass(string name, int num)
            {
                Name = name;
                Number = num;
            }
            public string Name { get; set; }
            public int Number { get; set; }
        }

        private List<DurationClass> _allProductsDurations = new List<DurationClass>()
        {
            new DurationClass("Месяц",0),
            new DurationClass("Квартал",1),
            new DurationClass("Год",2)
    
        };
        public List<DurationClass> AllProductsDurations
        {
            get
            {
                return _allProductsDurations;
            }
            set
            {
                _allProductsDurations = value;
            }
        }
        public DurationClass CurrentDuration { get; set; }

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
                    CurrentClient = new Client();
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

        private RelayCommand _deleteProduct;
        public RelayCommand DeleteProduct
        {
            get
            {
                return _deleteProduct ?? new RelayCommand(obj =>
                {
                    SQLHelper.DeleteProduct(CurrentProduct);
                    foreach(var tr in SQLHelper.GetAllTransactions())
                    {
                        if (tr.ProductId == CurrentProduct.Id) SQLHelper.DeleteTransaction(tr);
                    }
                    CurrentProduct = null;
                    UpdateAllTransactionsView();
                    UpdateAllProductsView();
                });
            }
        }
        
        private RelayCommand _deleteClient;
        public RelayCommand DeleteClient
        {
            get
            {
                return _deleteClient ?? new RelayCommand(obj =>
                {
                    SQLHelper.DeleteClient(CurrentClient);
                    CurrentClient = null;
                    UpdateAllClientsView();
                }
                );
            }
        }

        private RelayCommand _openEditProductWindow;
        public RelayCommand OpenEditProductWindow
        {
            get
            {
                return _openEditProductWindow ?? new RelayCommand(obj =>
                {
                    if(CurrentProduct != null)
                    OpenEditProductWindowMethod();
                });
            }
        }

        private RelayCommand _editProduct;
        public RelayCommand EditProduct
        {
            get
            {
                return _editProduct ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (SelectedProductName != null && 
                    SelectedProductName.Replace(" ", "").Length != 0 && 
                    SelectedProductPrice > 0 && 
                    SelectedProductType != null)
                    {
                        SQLHelper.EditProduct(CurrentProduct, SelectedProductName, SelectedProductPrice, (Model.Data.ProductType)SelectedProductType.Number);
                        wnd.Close();
                        UpdateAllProductsView();
                    }
                });
            }
        }

        private RelayCommand _openCurrentClientProductsWindow;
        public RelayCommand OpenCurrentClientProductsWindow
        {
            get
            {
                return _openCurrentClientProductsWindow ?? new RelayCommand(obj =>
                {
                    if(CurrentClient != null)
                    OpenCurrentClientProductsWindowMethod();
                });
            }
        }

        private RelayCommand _openAddProductToCurrentClient;
        public RelayCommand OpenAddProductToCurrentClient
        {
            get
            {
                return _openAddProductToCurrentClient ?? new RelayCommand(obj =>
                {
                    OpenAddProductToCurrentClientWindowMethod();
                });
            }
        }

        private RelayCommand _addProductTimeWindow;
        public RelayCommand AddProductTimeWindow
        {
            get
            {
                return _addProductTimeWindow ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                if (AllTransactions.Where(el => el.ProductId == CurrentProduct.Id && el.ClientId == CurrentClient.Id).Count() == 0 && SelectedProductType != null)
                    {
                        if (CurrentProduct.Type == Model.Data.ProductType.Limited)
                        {
                            wnd.Close();
                            OpenAddProductTimeWindowMethod();
                        }
                        else
                        {
                            wnd.Close();
                            SQLHelper.CreateTransaction(CurrentClient, CurrentProduct, Duration.Year);
                        }
                        UpdateAllTransactionsView();
                    }
                    else
                    {
                        MessageBox.Show("Такой товар уже есть у этого клиента","Ошибка добавления", MessageBoxButton.OK ,MessageBoxImage.Warning);
                    }
                });
            }
        }

        private RelayCommand _addProductToClient;
        public RelayCommand AddProductToClient
        {
            get
            {
                return _addProductToClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (CurrentDuration != null)
                    {
                        wnd.Close();
                        SQLHelper.CreateTransaction(CurrentClient, CurrentProduct,(Duration)CurrentDuration.Number);
                        UpdateAllTransactionsView();
                    }
                });
            }
        }

        private RelayCommand _deleteProductFromCurrentClient;
        public RelayCommand DeleteProductFromCurrentClient
        {
            get
            {
                return _deleteProductFromCurrentClient ?? new RelayCommand(obj =>
                {
                    //SQLHelper.DeleteTransaction(AllTransactions.Where(el=> el.ClientId == CurrentClient.Id && el.ProductId == CurrentProduct.Id).Single());
                    SQLHelper.DeleteTransaction(CurrentTransaction);
                    UpdateAllTransactionsView();
                });
            }
        }

        private RelayCommand _openEditCurrentClientWindow;
        public RelayCommand OpenEditCurrentClientWindow
        {
            get
            {
                return _openEditCurrentClientWindow ?? new RelayCommand(obj =>
                {
                    if(CurrentClient != null)
                    {
                        OpenEditCurrentClientWindowMethod();
                    }
                });
            }
        }
        private RelayCommand _editClient;
        public RelayCommand EditClient
        {
            get
            {
                return _editClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (ClientName != null &&
                    ClientName.Replace(" ", "").Length != 0 &&
                    ClientStatus != null)
                    {
                        SQLHelper.EditClient(CurrentClient, ClientName, CurrentManager.Id, (Model.Data.ClientStatus)ClientStatus.Number);
                        wnd.Close();
                        UpdateAllClientsView();
                    }
                });
            }
        }

        private RelayCommand _changeUser;
        public RelayCommand ChangeUser
        {
            get
            {
                return _changeUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    RegistrationWindow regWindow = new RegistrationWindow();
                    regWindow.Show();
                    wnd.Close();
                    
                });
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
        private void UpdateAllTransactionsView()
        {
            AllTransactions = SQLHelper.GetAllTransactions();
            CurrentClientsProducts.AllTransactions.ItemsSource = null;
            CurrentClientsProducts.AllTransactions.Items.Clear();
            CurrentClientsProducts.AllTransactions.ItemsSource = CurrentClientProducts;
            CurrentClientsProducts.AllTransactions.Items.Refresh();
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
            EditProductWindow editProductWindow = new EditProductWindow(this);
            SetCenterPositionAndOpen(editProductWindow);
        }
        private void OpenCurrentClientProductsWindowMethod()
        {
            CurrentClientsProducts currentClientsProductsWindow = new CurrentClientsProducts(this);
            SetCenterPositionAndOpen(currentClientsProductsWindow);
        }
        private void OpenAddProductToCurrentClientWindowMethod()
        {
            AddProductToCurrentClient addProductToCurrentClientWnd = new AddProductToCurrentClient(this);
            SetCenterPositionAndOpen(addProductToCurrentClientWnd);
        }
        private void OpenAddProductTimeWindowMethod()
        {
            AddProductTimeWindow addProductTimeWindow = new AddProductTimeWindow(this);
            SetCenterPositionAndOpen(addProductTimeWindow);
        }
        private void OpenEditCurrentClientWindowMethod()
        {
            EditClientWindow editClientWindow = new EditClientWindow(this);
            SetCenterPositionAndOpen(editClientWindow);
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
