namespace Inv.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Inv.Common.Models;
    using Inv.Services;
    using Inv.Views;
    
    public class ItemItemViewModel : Item
    {
        #region Attributes
        #endregion

        #region Services
        private ApiService apiService;
        #endregion

        #region Constructors
        public ItemItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand EditCountCommand
        {
            get
            {
                return new RelayCommand(EditCount);
            }
        }

        private async void EditCount()
        {
            //MainViewModel.GetInstance().EditCount = new EditCountViewModel(this);
            //await App.Navigator.PushAsync(new EditCountPage());
            MainViewModel.GetInstance().Counts = new CountsViewModel(this);
            await App.Navigator.PushAsync(new CountsPage());

        }
        #endregion

    }
}
