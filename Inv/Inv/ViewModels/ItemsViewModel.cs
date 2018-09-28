namespace Inv.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Inv.Helpers;
    using Inv.Models;
    using Inv.Views;
    using Services;
    using Xamarin.Forms;

    public class ItemsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Attributes
        private string filter;
        private bool isRefreshing;
        private bool isEnabled;

        //private ObservableCollection<CountItemViewModel> products;
        private ObservableCollection<ItemItemViewModel> items;
        #endregion

        #region Properties
        public List<Item> MyItems { get; set; }
        public List<ItemLocal> MyItemsLocal { get; set; }
        public ObservableCollection<ItemItemViewModel> Items
        {
            get { return this.items; }
            set { SetValue(ref this.items, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref filter, value);
                this.RefreshList();
            }
        }
        #endregion

        #region Singleton
        private static ItemsViewModel instance; // Atributo
        public static ItemsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ItemsViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public ItemsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadItems();
            //this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadItems()
        {
            this.IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                
                var answer = await this.LoadItemsFromAPI();
                if (answer)
                {
                    this.SaveItemsToSqlite();
                }
            } else
            {
                await this.LoadItemsFromDB();
            }

            if (this.MyItems == null || this.MyItems.Count == 0)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoItemsMessage, Languages.Accept);
                return;
            }
            this.RefreshList();
            this.IsRefreshing = false;
        }

        private async Task<bool> LoadItemsFromAPI()
        {
            //var response = await this.apiService.GetList<Product>("http://200.55.241.235", "/InvAPI/api", "/Products");
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlItemsController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            //var response = await this.apiService.GetList<Item>(url, prefix, controller);
            var response = await this.apiService.GetList<Item>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);


            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyItems = (List<Item>)response.Result; // hay que castearlo
            this.MyItemsLocal = this.MyItems.Select(P => new ItemLocal
            {
                Barcode = P.Barcode,
                ItemId = P.ItemId,
                Description = P.Description,
                IsAvailable = P.IsAvailable,
                MeasureUnitId = P.MeasureUnitId,
            }).ToList();
            return true;
        }

        private async Task SaveItemsToSqlite()
        {
            await this.dataService.DeleteAllItems();
            this.dataService.Insert(this.MyItemsLocal); // Nota: En este método no necesitamos el await.
        }

        private async Task LoadItemsFromDB()
        {
            this.MyItemsLocal = await this.dataService.GetAllItems();

            this.MyItems = this.MyItemsLocal.Select(p => new Item
            {
                ItemId = p.ItemId,
                Barcode = p.Barcode,
                Description = p.Description,
                IsAvailable = p.IsAvailable,
                MeasureUnitId = p.MeasureUnitId,
            }).ToList();
            //this.MyItems = await this.dataService.GetAllItems();
        }

        public void RefreshList()
        {
            //this.Items = new ObservableCollection<Item>(this.MyItems);

            if (string.IsNullOrEmpty(this.Filter))
            {
                // Expresion Lamda (ALTA PERFORMANCE)
                var myListItemItemViewModel = this.MyItems.Select(p => new ItemItemViewModel
                {
                    ItemId = p.ItemId,
                    Description = p.Description,
                    IsAvailable = p.IsAvailable,
                    Barcode = p.Barcode,
                    MeasureUnitId = p.MeasureUnitId,
                });
                this.Items = new ObservableCollection<ItemItemViewModel>(
                    myListItemItemViewModel.OrderBy(p => p.Description));
            }
            else
            {
                var myListItemItemViewModel = this.MyItems.Select(p => new ItemItemViewModel
                {
                    ItemId = p.ItemId,
                    Description = p.Description,
                    IsAvailable = p.IsAvailable,
                    Barcode = p.Barcode,
                    MeasureUnitId = p.MeasureUnitId,
                }).Where(p => p.Description.ToLower().Contains(this.Filter.ToLower())).ToList();
                this.Items = new ObservableCollection<ItemItemViewModel>(
                    myListItemItemViewModel.OrderBy(p => p.Description));
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadItems);
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        public ICommand NewCountCommand
        {
            get
            {
                return new RelayCommand(NewCount);
            }
        }
        private async void NewCount()
        {
            MainViewModel.GetInstance().NewCount = new NewCountViewModel();
            await App.Navigator.PushAsync(new NewCountPage());
        }


        #endregion
    }
}
