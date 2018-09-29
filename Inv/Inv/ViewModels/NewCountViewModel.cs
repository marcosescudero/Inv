namespace Inv.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Inv.Common.Models;
    using Inv.Interfaces;
    using Inv.Services;
    using Xamarin.Forms;

    public class NewCountViewModel : BaseViewModel
    {
        #region Attributes
        private string barcode;
        private bool isRunning;
        private bool isEnabled;
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
        public string Barcode
        {
            get { return this.barcode; }
            set { SetValue(ref this.barcode, value); }
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
