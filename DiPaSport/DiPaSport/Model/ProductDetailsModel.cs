using Com.IT.DiPaSport.Model.Customer;
using Com.IT.DiPaSport.Resources;
using DiPaSport;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class ProductDetailsModel : BaseModel
    {
        /// <summary>
        /// Fetches the product details.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        public void FetchProductDetails(string ID)
        {
            LoginObject login = null;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(Constants.JSON_TAG.LOGIN.ToString()))
            {
                login = (LoginObject)settings[Constants.JSON_TAG.LOGIN.ToString()];
            }

            string url = string.Format(Constants.URL_PRODUCT_DETAIL, ID, (login != null ? login.UserID : string.Empty));
            Execute(url);
        }

        /// <summary>
        /// Gets the product details.
        /// </summary>
        /// <param name="jsonData">The json data.</param>
        /// <returns></returns>
        public static ProductInfo GetProductDetails(JObject results)
        {
            if (results == null) return null;
            if ((int)results[Constants.JSON_TAG.ERROR_CODE] == 0)
            {
                JToken tmp = null;
                int groupId = (int)results[Constants.JSON_TAG.LOGIN.USER_GROUP_ID];
                LoginModel.UpdateGroupID(groupId);

                JObject data = (JObject)results[Constants.JSON_TAG.DATA];
                if (data == null) return null;

                string name = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.Codicedipa];
                string code = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.Name];
                string codeOem = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.CodeOem];
                string imagePath = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.Image];
                string entityID = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.Id];
                
                string priceTag = LoginModel.PriceType();
                float price = (float)data[priceTag];

                if (groupId == CustomerGroup.OFF_S)
                {
                    JArray prices = (JArray)data[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFF_S];
                    if (prices.Count() > 0)
                    {
                        foreach (var pp in prices)
                        {
                            int cus_gid = (int)pp["cust_group"];
                            if (groupId == cus_gid)
                            {
                                price = (float)pp["price"];
                            }
                        }
                    }
                }

                data.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PricePerCustomer, out tmp);
                float specialPrice;
                try
                {
                    specialPrice = tmp != null ? float.Parse(data[Constants.JSON_TAG.PRODUCT_DETAIL.PricePerCustomer].ToString(), CultureInfo.InvariantCulture.NumberFormat) : .0f;
                }
                catch (FormatException e)
                {
                    specialPrice = .0f;
                }

                if (price == 0)
                {
                    price = (float)data[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.NORMALE];
                }

                // Availability
                string availability = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.Availability_note];
                if (availability.Length == 0)
                {
                    string unavailable = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.UnAvailable];
                    if (unavailable.Length == 0)
                    {
                        string available = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.Available];
                        if (available.Length > 0)
                        {
                            availability = string.Format(AppResources.DetailProductAvailable, available);
                        }
                    }
                    else
                    {
                        availability = string.Format(AppResources.DetailProductUnavailble, unavailable);
                    }
                }

                if (App.CurrentLanguage == Languages.UK)
                {
                    if (availability.ToLower(CultureInfo.CurrentCulture).Equals("disponibile"))
                    {
                        availability = ("available").ToUpper(CultureInfo.CurrentCulture);
                    }
                }

                // Quick Overview
                string QuickOverView = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.QuickOverview];
                string SuitableFor = (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.Suitablefor];

                string PartCondition;
                data.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PartCondition, out tmp);
                PartCondition = tmp != null ? (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.PartCondition] : string.Empty;

                JArray MediaImage = (JArray)data[Constants.JSON_TAG.PRODUCT_DETAIL.Images];
                IList<string> imagesPath = new List<string>();
                foreach (var img in MediaImage)
                {
                    string path = img.ToString();
                    imagesPath.Add(path);
                }

                // References code
                data.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.RefCodes, out tmp);
                string RefCodes = tmp != null ? (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.RefCodes] : string.Empty;

                // Compatibility
                data.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.Compatibility, out tmp);
                string Compatibility = tmp != null ? (string)data[Constants.JSON_TAG.PRODUCT_DETAIL.Compatibility] : string.Empty;

                ProductInfo productInfo = new ProductInfo() { 
                    ID = entityID, 
                    Name = name, 
                    Code = code, 
                    CodeOEM = codeOem, 
                    ImagePath = imagePath, 
                    Price = price, 
                    SpecialPrice = specialPrice, 
                    Availability = availability, 
                    QuickOverview = QuickOverView, 
                    SuitableFor = SuitableFor, 
                    PartCondition = PartCondition, 
                    RefsCode = RefCodes, 
                    Compatibility = Compatibility, 
                    Images = new List<string>(imagesPath), 
                    RawJSON = data /* store current data*/ };

                return productInfo;
            }

            

            return null;
        }
    }
}
