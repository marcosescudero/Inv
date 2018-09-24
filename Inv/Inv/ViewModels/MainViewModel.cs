using System;
using System.Collections.Generic;
using System.Text;

namespace Inv.ViewModels
{
    public class MainViewModel
    {
        #region Atributes

        #endregion

        #region Properties

        #endregion

        #region ViewModels
        public ItemsViewModel Items { get; set; }
        public CountsViewModel Counts { get; set; }
        public LoginViewModel Login {get; set; }
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
            //this.Login = new LoginViewModel();
        }
        #endregion
    }
}
