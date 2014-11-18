using Com.IT.DiPaSport.Model;
using Com.IT.DiPaSport.Model.Customer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.ViewModel
{
    public class SearchViewModel
    {
        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public static ObservableCollection<ProductTable> Results { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is next page.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is next page; otherwise, <c>false</c>.
        /// </value>
        public static bool IsNextPage { get; set; }

        /// <summary>
        /// Converts the specified json object.
        /// </summary>
        /// <param name="jsonObj">The json object.</param>
        public static bool Convert(JObject jsonObj)
        {
            if (Results != null)
            {
                Results.Clear();
            }
            if (jsonObj != null)
            {
                if ((int)jsonObj[Constants.JSON_TAG.ERROR_CODE] == 0)
                {
                    int gid = (int)jsonObj[Constants.JSON_TAG.LOGIN.USER_GROUP_ID];
                    LoginModel.UpdateGroupID(gid);

                    JObject paging = (JObject)jsonObj[Constants.JSON_TAG.PRODUCT_SEARCHING.PAGING];
                    IsNextPage = paging != null ? (bool)paging[Constants.JSON_TAG.PRODUCT_SEARCHING.NEXT_PAGE] : false; 

                    JArray data = (JArray)jsonObj[Constants.JSON_TAG.DATA];
                    Results = new ObservableCollection<ProductTable>();
                    foreach (JObject p in data)
                    {
                        ProductTable product = new ProductTable()
                        {
                            ID = (string)p[Constants.JSON_TAG.PRODUCT_DETAIL.Id],
                            Name = (string)p[Constants.JSON_TAG.PRODUCT_DETAIL.Codicedipa],
                            Code = (string)p[Constants.JSON_TAG.PRODUCT_DETAIL.Name],
                            CodeOEM = (string)p[Constants.JSON_TAG.PRODUCT_DETAIL.CodeOem],
                            ImagePath = (string)p[Constants.JSON_TAG.PRODUCT_DETAIL.Image],
                        };

                        if (!product.ImagePath.EndsWith("png") && !product.ImagePath.EndsWith("jpg"))
                        {
                            product.ImagePath = "/Asserts/Images/logo.png";
                        }

                        string priceTag = LoginModel.PriceType();
                        float price = (float)p[priceTag];

                        if (gid == CustomerGroup.OFF_S)
                        {
                            JArray prices = (JArray)p[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFF_S];
                            if (prices.Count() > 0)
                            {
                                foreach (var pp in prices)
                                {
                                    int cus_gid = (int)pp["cust_group"];
                                    if (gid == cus_gid)
                                    {
                                        price = (float)pp["price"];
                                    }
                                }
                            }
                        }

                        float specialPrice;
                        JToken tmp;
                        p.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PricePerCustomer, out tmp);
                        try
                        {
                            string sp = (string)p[Constants.JSON_TAG.PRODUCT_DETAIL.PricePerCustomer];
                            specialPrice = tmp != null ? float.Parse(sp, CultureInfo.InvariantCulture.NumberFormat) : .0f;
                        }
                        catch (FormatException e)
                        {
                            specialPrice = .0f;
                        }

                        if (price == 0)
                        {
                            price = (float)p[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.NORMALE];
                        }

                        product.Price = price;
                        product.SpecialPrice = specialPrice;

                        Results.Add(product);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
