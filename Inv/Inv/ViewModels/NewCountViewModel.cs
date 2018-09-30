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
        private int itemId;

        private bool isRunning;
        private bool isEnabled;
        private Item item;
        #endregion

        #region Services
        private ApiService apiService;
        private Location locationSelected;
        private MeasureUnit measureUnitSelected;
        #endregion

        #region Properties
        public List<Item> Items { get; set; }
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
        public int ItemId
        {
            get { return this.itemId; }
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

        #endregion

        #region Constructors
        public NewCountViewModel()
        {

            this.IsRunning = false;
            this.IsEnabled = true;
            this.apiService = new ApiService();
            LocationSelected = new Location();

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
            if (this.ItemId != 0)
            {
                this.Item = MainViewModel.GetInstance().
                    Items.MyItems.Where(p => p.ItemId == this.ItemId).FirstOrDefault();
                if (this.Item != null)
                {
                    //await Application.Current.MainPage.DisplayAlert("Eureka", this.Item.Description, Languages.Accept);
                    // Le coloca la U.M. por Default
                    this.MeasureUnitSelected = this.MyMeasureUnits.Where(p => p.MeasureUnitId == this.Item.MeasureUnitId).FirstOrDefault();
                    if (string.IsNullOrEmpty(this.Barcode))
                    {
                        this.Barcode = this.Item.Barcode;
                    }
                }
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
            if (this.Item != null)
                if (!string.IsNullOrEmpty(this.Barcode)) 
                    if (this.Item.Barcode == this.Barcode)
                        return;

            if (!string.IsNullOrEmpty(this.Barcode))
            {
                this.Item = MainViewModel.GetInstance().
                    Items.MyItems.Where(p => p.Barcode == this.Barcode).FirstOrDefault();
                if (this.Item != null)
                {
                    //await Application.Current.MainPage.DisplayAlert("Eureka", this.Item.Description, Languages.Accept);
                    // Le coloca la U.M. por Default
                    this.MeasureUnitSelected = this.MyMeasureUnits.Where(p => p.MeasureUnitId == this.Item.MeasureUnitId).FirstOrDefault();
                }
            }
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
