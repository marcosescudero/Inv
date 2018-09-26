namespace Inv.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Inv.Common.Models;
    using Inv.Helpers;
    using Inv.Interfaces;
    using Inv.Models;
    using Inv.Services;
    using Xamarin.Forms;

    public class NewCountViewModel :BaseViewModel
    {
        #region Attributes
        private string barcode;
        private bool isRunning;
        private bool isEnabled;
        //private MeasureUnitLocal measureUnitSelectedIndex;
        #endregion

        #region Services
        private ApiService apiService;
        #endregion

        #region Properties
        public List<Item> Items { get; set; }
        public List<MeasureUnit> MeasureUnits { get; set; }
        public List<MeasureUnitLocal> MySqliteMeasureUnits { get; set; }
        public List<Location> Locations { get; set; }
        public List<Bin> Bins { get; set; }
        public Count NewCount { get; set; }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public string Barcode
        {
            get { return this.barcode; }
            set { SetValue(ref this.barcode, value); }
        }

        /*
        public MeasureUnitLocal MeasureUnitSelectedIndex
        {
            get { return this.measureUnitSelectedIndex; }
            set { SetValue(ref this.measureUnitSelectedIndex, value); }
        }
        */
        #endregion

        #region Constructors
        public NewCountViewModel()
        {
            this.apiService = new ApiService();
            LoadMeasureUnits();
        }
        #endregion

        #region Methods
        private async void LoadMeasureUnits()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                var answer = await this.LoadMeasuresFromAPI();
                if (answer)
                {
                    //this.SaveMeasuresToDB();
                }
            }
            else
            {
                //await this.LoadMeasuresFromDB();
            }

            if (this.MeasureUnits == null || this.MeasureUnits.Count == 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoCountsMessage, Languages.Accept);
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;
        }

        private async Task<bool> LoadMeasuresFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlMeasureUnitsController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<MeasureUnit>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.MeasureUnits = (List<MeasureUnit>)response.Result; // hay que castearlo
            this.MySqliteMeasureUnits = this.MeasureUnits.Select(P => new MeasureUnitLocal
            {
                MeasureUnitId = P.MeasureUnitId,
                Description = P.Description,
            }).ToList();
            return true;
        }
        #endregion

        #region Commands
        public ICommand ScanCommand
        {
            get
            {
                return new RelayCommand(Scan);
            }
        }

        private async void Scan()
        {
            this.Barcode = await ScannerSKU();
        }
        public async Task<string> ScannerSKU()
        {
            try
            {
                var scanner = DependencyService.Get<IQrCodeScanningService>();
                var result = await scanner.ScanAsync();
                return result.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }
        #endregion

    }
}
