[assembly: Xamarin.Forms.Dependency(typeof(Inv.Droid.QrCodeScanningService))]
namespace Inv.Droid
{
    using System.Threading.Tasks;
    using Inv.Interfaces;
    using ZXing.Mobile;
    public class QrCodeScanningService : IQrCodeScanningService
    {
        public async Task<string> ScanAsync()
        {
            var options = new MobileBarcodeScanningOptions();
            var scanner = new MobileBarcodeScanner();
            var scanResults = await scanner.Scan(options);
            return scanResults.Text;
        }
    }
}
