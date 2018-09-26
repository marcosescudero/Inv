using Inv.Common.Models;
using Inv.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Inv.ViewModels
{
    public class EditCountViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private Item item;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public Item Item
        {
            get { return this.item; }
            set { SetValue(ref this.item, value); }
        }
        public List<Count> MyItemCount { get; set; }
        #endregion

        #region Constructors
        public EditCountViewModel(ItemItemViewModel item)
        {
            this.item = item;
            this.LoadItemCounts();
        }

        private void LoadItemCounts()
        {
            
        }
        #endregion
    }
}
