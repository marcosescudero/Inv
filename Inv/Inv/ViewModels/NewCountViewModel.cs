

using GalaSoft.MvvmLight.Command;
using Inv.Common.Models;
using Inv.Interfaces;
using Inv.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inv.ViewModels
{
    public class NewCountViewModel :BaseViewModel
    {
        #region Attributes
        private string barcode;
        #endregion

        #region Services
        private ApiService apiService;
        #endregion

        #region Properties
        public List<Item> Items { get; set; }
        public List<MeasureUnit> MeasureUnits { get; set; }
        public List<Location> Locations { get; set; }
        public List<Bin> Bins{ get; set; }
        
        public string Barcode
        {
            get { return this.barcode; }
            set { SetValue(ref this.barcode, value); }
        }

        #endregion

        #region Constructors
        public NewCountViewModel()
        {
            this.apiService = new ApiService();
        }
        public NewCountViewModel(bool scan)
        {
            this.apiService = new ApiService();
            Scan();
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
