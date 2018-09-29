namespace Inv.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Helpers;

    public class MainViewModel
    {
        #region Atributes

        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public MeasureUnitsViewModel MeasureUnits { get; set; }
        public LocationsViewModel Locations { get; set; }
        //public BinsViewModel Bins { get; set; }
        public ItemsViewModel Items { get; set; }
        public CountsViewModel Counts { get; set; }
        public EditCountViewModel EditCount { get; set; }
        public NewCountViewModel NewCount { get; set; }
        #endregion

        #region Singleton
        private static MainViewModel instance; // Atributo
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.LoadMenu();
        }

        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = Languages.About,
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_phonelink_setup",
                PageName = "SetupPage",
                Title = Languages.Setup,
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = Languages.Exit,
            });
        }


        #endregion
    }
}
