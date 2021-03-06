﻿
namespace Inv.Helpers
{
    using Inv.Interfaces;
    using Xamarin.Forms;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }
        public static string Products
        {
            get { return Resource.Products; }
        }
        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }
        public static string AddProduct
        {
            get { return Resource.AddProduct; }
        }
        public static string Description
        {
            get { return Resource.Description; }
        }
        public static string DescriptionPlaceholder
        {
            get { return Resource.DescriptionPlaceholder; }
        }
        public static string Price
        {
            get { return Resource.Price; }
        }
        public static string PricePlaceholder
        {
            get { return Resource.PricePlaceholder; }
        }
        public static string Remarks
        {
            get { return Resource.Remarks; }
        }
        public static string Save
        {
            get { return Resource.Save; }
        }
        public static string ChangeImage
        {
            get { return Resource.ChangeImage; }
        }
        public static string DescriptionError
        {
            get { return Resource.DescriptionError; }
        }
        public static string PriceError
        {
            get { return Resource.PriceError; }
        }
        public static string ImageSource
        {
            get { return Resource.ImageSource; }
        }

        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }

        public static string NewPicture
        {
            get { return Resource.NewPicture; }
        }

        public static string Cancel
        {
            get { return Resource.Cancel; }
        }
        public static string Delete
        {
            get { return Resource.Delete; }
        }

        public static string Edit
        {
            get { return Resource.Edit; }
        }

        public static string DeleteConfirmation
        {
            get { return Resource.DeleteConfirmation; }
        }

        public static string Yes
        {
            get { return Resource.Yes; }
        }

        public static string No
        {
            get { return Resource.No; }
        }
        public static string Confirm
        {
            get { return Resource.Confirm; }
        }
        public static string EditProduct
        {
            get { return Resource.EditProduct; }
        }
        public static string IsAvailable
        {
            get { return Resource.IsAvailable; }
        }
        public static string Search
        {
            get { return Resource.Search; }
        }

        public static string Login
        {
            get { return Resource.Login; }
        }

        public static string EMail
        {
            get { return Resource.EMail; }
        }

        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string Password
        {
            get { return Resource.Password; }
        }

        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }

        public static string Forgot
        {
            get { return Resource.Forgot; }
        }

        public static string Register
        {
            get { return Resource.Register; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }

        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }

        public static string SomethingWrong
        {
            get { return Resource.SomethingWrong; }
        }

        public static string Menu
        {
            get { return Resource.Menu; }
        }

        public static string Setup
        {
            get { return Resource.Setup; }
        }

        public static string About
        {
            get { return Resource.About; }
        }

        public static string Exit
        {
            get { return Resource.Exit; }
        }
        
        public static string InventoryCount
        {
            get { return Resource.InventoryCount; }
        }
        public static string NoCountsMessage
        {
            get { return Resource.NoCountsMessage; }
        }
        public static string ItemsList
        {
            get { return Resource.ItemsList; }
        }
        public static string Scan
        {
            get { return Resource.Scan; }
        }

        public static string New
        {
            get { return Resource.New; }
        }
        public static string NoItemsMessage
        {
            get { return Resource.NoItemsMessage; }
        }
        public static string NoLocationsMessage
        {
            get { return Resource.NoLocationsMessage; }
        }
        public static string NoBinsMessage
        {
            get { return Resource.NoBinsMessage; }
        }
        public static string ItemIdEmpty
        {
            get { return Resource.ItemIdEmpty; }
        }
        public static string MeasureUnitEmpty
        {
            get { return Resource.MeasureUnitEmpty; }
        }
        public static string LocationEmpty
        {
            get { return Resource.LocationEmpty; }
        }
        public static string QuantityNotValid
        {
            get { return Resource.QuantityNotValid; }
        }
        public static string QuantityEmpty
        {
            get { return Resource.QuantityEmpty; }
        }
        public static string CountConfirmation
        {
            get { return Resource.CountConfirmation; }
        }
        public static string ItemNonExist
        {
            get { return Resource.ItemNonExist; }
        }
        public static string DataSaved
        {
            get { return Resource.DataSaved; }
        }

    }
}
