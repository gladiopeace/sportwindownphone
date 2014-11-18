using Com.IT.DiPaSport.Database;
using Com.IT.DiPaSport.Model;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.ViewModel
{
    public class CartOrderViewModel
    {
        /// <summary>
        /// The order
        /// </summary>
        private OrderProducts Order;

        public CartOrderViewModel()
        {
            Order = new OrderProducts();
        }

        public IList<ProductTable> GetOrder()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(Constants.JSON_TAG.LOGIN.ToString()))
            {
                LoginObject userLogin = (LoginObject)settings[Constants.JSON_TAG.LOGIN.ToString()];
                if (userLogin != null)
                {
                    IList<ProductTable> order = Order.GetProducts(userLogin.UserID);
                    return order;
                }
            }

            return new List<ProductTable>();
        }

        public void ClearOrder()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(Constants.JSON_TAG.LOGIN.ToString()))
            {
                LoginObject userLogin = (LoginObject)settings[Constants.JSON_TAG.LOGIN.ToString()];
                if (userLogin != null)
                {
                    Order.DeleteAllProducts(userLogin.UserID);
                }
            }
        }
    }
}
