namespace Inv.Interfaces
{
    using System.Threading.Tasks;
    public interface IQrCodeScanningService
    {
        Task<string> ScanAsync();
    }

}
