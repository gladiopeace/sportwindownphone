using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class ProductFeaturesModel : BaseModel
    {
        public void ProductFeatures()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            string userID = "";
            if (settings.Contains(Constants.JSON_TAG.LOGIN.ToString()))
            {
                LoginObject login = (LoginObject)settings[Constants.JSON_TAG.LOGIN.ToString()];
                if (login != null)
                {
                    userID = login.UserID;
                }
            }
            string url = string.Format(Constants.URL_PRODUCT_FEATURES, userID);
            Execute(url);
        }
    }
}
