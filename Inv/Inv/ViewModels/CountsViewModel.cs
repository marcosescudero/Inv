
namespace Inv.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using Xamarin.Forms;

    public class CountsViewModel : BaseViewModel
    {
        #region Attributes
        private string filter;
        private ApiService apiService;
        private bool isRefreshing;
        //private ObservableCollection<CountItemViewModel> products;
        private ObservableCollection<Count> counts;
        #endregion

        #region Properties
        public List<Count> MyCounts { get; set; }

        public ObservableCollection<Count> Counts
        {
            get { return this.counts; }
            set { SetValue(ref this.counts, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Singleton
        private static CountsViewModel instance; // Atributo
        public static CountsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new CountsViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public CountsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadCounts();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadCounts()
        {
            this.IsRefreshing = true;
            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                var answer = await this.LoadCountsFromAPI();
                if (answer)
                {
                    //this.SaveProductsToDB();
                }
            }

            if (this.MyCounts == null || this.MyCounts.Count == 0)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoCountsMessage, Languages.Accept);
                return;
            }

            this.RefreshList();
            this.IsRefreshing = false;

        }

        private async Task<bool> LoadCountsFromAPI()
        {
            //var response = await this.apiService.GetList<Product>("http://200.55.241.235", "/InvAPI/api", "/Products");
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlCountsController"].ToString(); // Obtengo el controlador del diccionario de recursos.

            //var response = await this.apiService.GetList<Count>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            var response = await this.apiService.GetList<Count>(url, prefix, controller);

            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyCounts = (List<Count>) response.Result; // hay que castearlo
            return true;
        }

        public void RefreshList()
        {
            this.Counts = new ObservableCollection<Count>(this.MyCounts);
        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCounts);
            }
        }
        #endregion
    }
}
