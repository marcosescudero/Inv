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
        private MeasureUnit measureUnitSelectedIndex;
        #endregion

        #region Services
        private ApiService apiService;
        #endregion

        #region Properties
        public List<Item> Items { get; set; }
        public List<MeasureUnit> MyMeasureUnits { get; set; }
        public List<MeasureUnitLocal> MeasureUnitsLocal { get; set; }
        public List<Location> Locations { get; set; }
        public List<Bin> Bins { get; set; }
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

        
        public MeasureUnit MeasureUnitSelectedIndex
        {
            get { return this.measureUnitSelectedIndex; }
            set { SetValue(ref this.measureUnitSelectedIndex, value); }
        }
        
        #endregion

        #region Constructors
        public NewCountViewModel()
        {
            this.IsRunning = false;
            this.IsEnabled = true;
            this.apiService = new ApiService();
            this.MyMeasureUnits = MainViewModel.GetInstance().MeasureUnits.MyMeasureUnits;
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
