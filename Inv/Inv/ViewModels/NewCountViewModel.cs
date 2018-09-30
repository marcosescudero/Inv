namespace Inv.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Inv.Common.Models;
    using Inv.Helpers;
    using Inv.Interfaces;
    using Inv.Services;
    using Xamarin.Forms;

    public class NewCountViewModel : BaseViewModel
    {
        #region Attributes
        private string barcode;
        private string itemId;
        private string quantity;

        private bool isRunning;
        private bool isEnabled;
        private bool isBarcodeEntryEnabled;
        private bool stopPropagation;
        private Item item;
        #endregion

        #region Services
        private ApiService apiService;
        private Location locationSelected;
        private MeasureUnit measureUnitSelected;
        #endregion

        #region Properties
        //public List<Item> Items { get; set; }
        public List<MeasureUnit> MyMeasureUnits { get; set; }
        public List<Location> MyLocations { get; set; }
        public ObservableCollection<MeasureUnit> MeasureUnits { get; set; }
        public ObservableCollection<Location> Locations { get; set; }
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
        public string ItemId
        {
            get {
                if (this.itemId == null)
                    return "";
                else
                    return this.itemId.ToString();
            }
            set { SetValue(ref this.itemId, value); }
        }
        public string Barcode
        {
            get { return this.barcode; }
            set { SetValue(ref this.barcode, value); }
        }
        public Item Item
        {
            get { return this.item; }
            set { SetValue(ref this.item, value); }
        }
        public Location LocationSelected
        {
            get { return this.locationSelected; }
            set { SetValue(ref this.locationSelected, value); }
        }
        public MeasureUnit MeasureUnitSelected
        {
            get { return this.measureUnitSelected; }
            set { SetValue(ref this.measureUnitSelected, value); }
        }
        public string Quantity
        {
            get
            {
                if (this.quantity == null)
                    return "";
                else
                    return this.quantity.ToString();
            }
            set { SetValue(ref this.quantity, value); }
        }
        public bool IsBarcodeEntryEnabled
        {
            get { return this.isBarcodeEntryEnabled; }
            set { SetValue(ref this.isBarcodeEntryEnabled, value); }
        }
        #endregion

        #region Constructors
        public NewCountViewModel()
        {
            this.IsRunning = false;
            this.IsEnabled = true;
            this.apiService = new ApiService();
            this.LocationSelected = new Location();
            this.IsBarcodeEntryEnabled = false; // Barcode Entry disable 

            // Measure units
            this.MyMeasureUnits = MainViewModel.GetInstance().
                MeasureUnits.MyMeasureUnits.Select(p => new MeasureUnit
                {
                    MeasureUnitId = p.MeasureUnitId,
                    Description = p.Description,
                }).ToList();
            this.MeasureUnits = new ObservableCollection<MeasureUnit>(this.MyMeasureUnits);

            // Locations
            this.MyLocations = MainViewModel.GetInstance().
                Locations.MyLocations.Select(p => new Location
                {
                    LocationId = p.LocationId,
                    Description = p.Description,
                }).ToList();
            this.Locations = new ObservableCollection<Location>(this.MyLocations);
        }
        #endregion

        #region Commands

        public ICommand ItemIdChangedCommand
        {
            get
            {
                return new RelayCommand(ItemIdChanged);
            }
        }

        private void ItemIdChanged()
        {

            if (this.stopPropagation)
            {
                this.stopPropagation = false;
                return;
            }

            var id =0;
            try { id = int.Parse(this.ItemId);}
            catch { id = 0; }

            if (id != 0)
            {
                this.Item = MainViewModel.GetInstance().
                    Items.MyItems.Where(p => p.ItemId == id).FirstOrDefault();
                if (this.Item != null)
                {
                    //await Application.Current.MainPage.DisplayAlert("Eureka", this.Item.Description, Languages.Accept);
                    // Le coloca la U.M. por Default
                    this.MeasureUnitSelected = this.MyMeasureUnits.Where(p => p.MeasureUnitId == this.Item.MeasureUnitId).FirstOrDefault();
                    this.stopPropagation = true;
                    this.Barcode = this.Item.Barcode;
                } else
                {
                    this.stopPropagation = true;
                    this.Barcode = string.Empty;
                    CleanForm();
                }
            }
            else
            {
                this.stopPropagation = true;
                this.Barcode = string.Empty;
                CleanForm();
            }
        }

        public ICommand BarcodeChangedCommand
        {
            get
            {
                return new RelayCommand(BarcodeChanged);
            }
        }

        private void BarcodeChanged()
        {

            if (this.stopPropagation)
            {
                this.stopPropagation = false;
                return;
            }

            if (!string.IsNullOrEmpty(this.Barcode))
            {
                
                this.Item = MainViewModel.GetInstance().
                    Items.MyItems.Where(p => p.Barcode == this.Barcode).FirstOrDefault();
                if (this.Item != null)
                {
                    //await Application.Current.MainPage.DisplayAlert("Eureka", this.Item.Description, Languages.Accept);
                    // Le coloca la U.M. por Default
                    this.MeasureUnitSelected = this.MyMeasureUnits.Where(p => p.MeasureUnitId == this.Item.MeasureUnitId).FirstOrDefault();
                    this.stopPropagation = true;
                    this.ItemId = this.Item.ItemId.ToString();
                } else
                {
                    this.stopPropagation = true;
                    this.ItemId = string.Empty;
                    CleanForm();
                }
            } 
        }

        public void CleanForm()
        {
            this.MeasureUnitSelected = null;
            this.LocationSelected = null;
            this.Item = null;
            //this.ItemId = string.Empty;
            //this.Barcode = string.Empty;
            this.Quantity = null;
        }

        public ICommand ScanCommand
        {
            get
            {
                return new RelayCommand(Scan);
            }
        }

        private async void Scan()
        {
            this.IsEnabled = false;
            this.Barcode = await ScannerSKU();
            this.IsEnabled = true;
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

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            this.IsEnabled = false;


            if (this.Item == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ItemNonExist,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.ItemId))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.ItemIdEmpty, 
                    Languages.Accept);
                return;
            }
            if (this.MeasureUnitSelected==null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.MeasureUnitEmpty, 
                    Languages.Accept);
                return;
            }
            if (this.LocationSelected == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.LocationEmpty, 
                    Languages.Accept);
                return;
            }
            if (this.Quantity == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.QuantityEmpty, 
                    Languages.Accept);
                return;
            }
            if (this.Quantity == "")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.QuantityEmpty, 
                    Languages.Accept);
                return;
            }
            decimal qty;
            try
            {
                qty = decimal.Parse(this.Quantity);
            }
            catch
            {
                qty = -1;
            }
            if (qty < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.QuantityNotValid, 
                    Languages.Accept);
                return;
            }

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            //await Application.Current.MainPage.DisplayAlert("OK", "DATOS VALIDOS", Languages.Accept);
            var result = await Application.Current.MainPage.DisplayAlert(
            Languages.Confirm,
            Languages.CountConfirmation,
            Languages.Accept,
            Languages.Cancel);

            if (!result)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            this.IsEnabled = false;
            this.IsRunning = true;

            var count = new Count
            {
                ItemId = int.Parse(this.ItemId),
                MeasureUnitId = this.MeasureUnitSelected.MeasureUnitId,
                LocationId = this.LocationSelected.LocationId,
                CountDate = DateTime.Now,
                UserName = Settings.UserName,
                Quantity = qty,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlCountsController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, count, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.DataSaved,
                Languages.Accept);

            await App.Navigator.PopAsync();

        }

        #endregion

    }
}
