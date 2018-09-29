namespace Inv.ViewModels
{
    using Inv.Common.Models;
    using Inv.Helpers;
    using Inv.Models;
    using Inv.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class LocationsViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isEnabled;
        #endregion

        #region Properties
        public List<Location> MyLocations { get; set; }
        public List<LocationLocal> MyLocationsLocal { get; set; }
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
        #endregion

        #region Singleton
        private static LocationsViewModel instance; // Atributo
        public static LocationsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new LocationsViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public LocationsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadLocations();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadLocations()
        {
            this.IsRefreshing = true;
            this.IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess)
            {

                var answer = await this.LoadLocationsFromAPI();
                if (answer)
                {
                    this.SaveLocationsToSqlite();
                }
            }
            else
            {
                await this.LoadLocationsFromDB();
            }

            if (this.MyLocations == null || this.MyLocations.Count == 0)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoLocationsMessage, Languages.Accept);
                return;
            }

            this.IsRefreshing = false;
            this.IsEnabled = true;
        }

        private async Task<bool> LoadLocationsFromAPI()
        {
            //var response = await this.apiService.GetList<Product>("http://200.55.241.235", "/InvAPI/api", "/Products");
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlLocationsController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<Location>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyLocations = (List<Location>)response.Result; // hay que castearlo
            this.MyLocationsLocal = this.MyLocations.Select(P => new LocationLocal
            {
                LocationId = P.LocationId,
                Description = P.Description,
            }).ToList();
            return true;
        }

        private async Task SaveLocationsToSqlite()
        {
            await this.dataService.DeleteAllLocations();
            this.dataService.Insert(this.MyLocationsLocal); // Nota: En este método no necesitamos el await.
        }

        private async Task LoadLocationsFromDB()
        {
            this.MyLocationsLocal = await this.dataService.GetAllLocations();

            this.MyLocations = this.MyLocationsLocal.Select(p => new Location
            {
                LocationId = p.LocationId,
                Description = p.Description,
            }).ToList();
        }
        #endregion
    }
}

