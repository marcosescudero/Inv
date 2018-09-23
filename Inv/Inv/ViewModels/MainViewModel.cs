using System;
using System.Collections.Generic;
using System.Text;

namespace Inv.ViewModels
{
    public class MainViewModel
    {
        #region Properties
        public ItemsViewModel Items { get; set; }
        public CountsViewModel Counts { get; set; }
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
        }
        #endregion
    }
}
