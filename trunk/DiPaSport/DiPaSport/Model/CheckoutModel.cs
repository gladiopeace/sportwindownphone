using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class CheckoutModel : BaseModel
    {
        /// <summary>
        /// Checkouts the specified products.
        /// </summary>
        /// <param name="products">The products.</param>
        public void Checkout(IList<ProductTable> products)
        {
            Checkout(products, string.Empty);
        }

        /// <summary>
        /// Checkouts the specified products.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="couponCode">The coupon code.</param>
        public void Checkout(IList<ProductTable> products, string couponCode)
        {
            string orderItems = "";
            LoginObject login = LoginModel.RetrieveLoginSettings();
            for (int i = 0; i < products.Count; i++)
            {
                orderItems += string.Format("{0}:{1}", products[i].ID, products[i].Quantity);
                if (i < (products.Count - 1))
                {
                    orderItems += "|";
                }
            }

            string url = string.Format(Constants.URL_ORDER, login.UserEmail, login.UserID, orderItems);
            url = url.Replace("|", "%7C");

            if (couponCode.Length > 0)
            {
                url += ("&couponCode=" + couponCode);
            }

            Execute(url);
        }
    }
}
