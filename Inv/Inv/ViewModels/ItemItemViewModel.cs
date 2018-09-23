using Inv.Common.Models;
using Inv.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inv.ViewModels
{
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

    }
}
