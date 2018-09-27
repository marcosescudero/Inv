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

    public class MeasureUnitsViewModel:BaseViewModel
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
        public List<MeasureUnit> MyMeasureUnits { get; set; }
        public List<MeasureUnitLocal> MyMeasureUnitsLocal { get; set; }
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
        private static MeasureUnitsViewModel instance; // Atributo
        public static MeasureUnitsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MeasureUnitsViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public MeasureUnitsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadMeasureUnits();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadMeasureUnits()
        {
            this.IsRefreshing = true;
            this.IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess)
            {

                var answer = await this.LoadMeasureUnitsFromAPI();
                if (answer)
                {
                    this.SaveMeasureUnitsToSqlite();
                }
            }
            else
            {
                await this.LoadMeasureUnitsFromDB();
            }

            if (this.MyMeasureUnits == null || this.MyMeasureUnits.Count == 0)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoMeasureUnitsMessage, Languages.Accept);
                return;
            }

            this.IsRefreshing = false;
            this.IsEnabled = true;
        }

        private async Task<bool> LoadMeasureUnitsFromAPI()
        {
            //var response = await this.apiService.GetList<Product>("http://200.55.241.235", "/InvAPI/api", "/Products");
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlMeasureUnitsController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<MeasureUnit>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyMeasureUnits = (List<MeasureUnit>)response.Result; // hay que castearlo
            this.MyMeasureUnitsLocal = this.MyMeasureUnits.Select(P => new MeasureUnitLocal
            {
                MeasureUnitId = P.MeasureUnitId,
                Description = P.Description,
            }).ToList();
            return true;
        }

        private async Task SaveMeasureUnitsToSqlite()
        {
            await this.dataService.DeleteAllMeasureUnits();
            this.dataService.Insert(this.MyMeasureUnitsLocal); // Nota: En este método no necesitamos el await.
        }

        private async Task LoadMeasureUnitsFromDB()
        {
            this.MyMeasureUnitsLocal = await this.dataService.GetAllMeasureUnits();

            this.MyMeasureUnits = this.MyMeasureUnitsLocal.Select(p => new MeasureUnit
            {
                MeasureUnitId = p.MeasureUnitId,
                Description = p.Description,
            }).ToList();
            //this.MyItems = await this.dataService.GetAllItems();
        }
        #endregion
    }
}
