using Com.IT.DiPaSport.Model.Customer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;

namespace Com.IT.DiPaSport.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginModel : BaseModel
    {
        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public void Login(string email, string password)
        {
            string url_login = String.Format(Constants.URL_LOGIN, new string[] { email, password });
            Execute(url_login);
        }

        /// <summary>
        /// Saves the login settings.
        /// </summary>
        /// <param name="data">The data.</param>
        public static void SaveLoginSettings(JObject data)
        {
            string userID = (string)data[Constants.JSON_TAG.LOGIN.USER_ID];
            string userName = (string)data[Constants.JSON_TAG.LOGIN.USER_NAME];
            int group = (int)data[Constants.JSON_TAG.LOGIN.USER_GROUP_ID];
            string company = (string)data[Constants.JSON_TAG.LOGIN.USER_COMPANY];
            string telephone = (string)data[Constants.JSON_TAG.LOGIN.USER_TELEPHONE];
            string email = (string)data[Constants.JSON_TAG.LOGIN.USER_EMAIL];

            LoginObject loginObj = new LoginObject
            {
                UserID = userID,
                UserName = userName,
                UserGroup = group,
                UserCompany = company,
                UserTelephone = telephone,
                UserEmail = email
            };

            // Store settings
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Add(Constants.JSON_TAG.LOGIN.ToString(), loginObj);
            settings.Save();
        }

        /// <summary>
        /// Retrieves the login settings.
        /// </summary>
        /// <returns></returns>
        public static LoginObject RetrieveLoginSettings()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            LoginObject loginSettings = null;
            settings.TryGetValue<LoginObject>(Constants.JSON_TAG.LOGIN.ToString(), out loginSettings);
            return loginSettings;
        }


        public static int Group
        {
            get
            {
                LoginObject lo = RetrieveLoginSettings();
                if (lo != null)
                {
                    return lo.UserGroup;
                }
                return CustomerGroup.NOPREZZI;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is login.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is login; otherwise, <c>false</c>.
        /// </value>
        public static bool IsLogin
        {
            get
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                return settings.Contains(Constants.JSON_TAG.LOGIN.ToString());
            }
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public static void Logout()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Remove(Constants.JSON_TAG.LOGIN.ToString());
            settings.Save();
        }

        /// <summary>
        /// Prices the is available.
        /// </summary>
        /// <returns></returns>
        public static bool priceIsAvailable()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(Constants.JSON_TAG.LOGIN.USER_GROUP_ID))
            {
                int gid = (int)settings[Constants.JSON_TAG.LOGIN.USER_GROUP_ID];
                bool g0 = (gid == CustomerGroup.NOT_LOGGED_IN) ? false : true;
                bool g16 = (gid == CustomerGroup.NOPREZZI) ? false : true;
                return (g0 && g16);
            }
            return false;
        }

        /// <summary>
        /// Updates the group identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static bool UpdateGroupID(int id)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings[Constants.JSON_TAG.LOGIN.USER_GROUP_ID] = id;
            LoginObject loginObj = null;
            settings.TryGetValue<LoginObject>(Constants.JSON_TAG.LOGIN.ToString(), out loginObj);
            if (loginObj != null)
            {
                loginObj.UserGroup = id;
                settings[Constants.JSON_TAG.LOGIN.ToString()] = loginObj;
            }
            settings.Save();
            return true;
        }

        /// <summary>
        /// Prices the type.
        /// </summary>
        /// <returns></returns>
        public static string PriceType()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            int gid = (int)settings[Constants.JSON_TAG.LOGIN.USER_GROUP_ID];
            if(gid == CustomerGroup.OFFICINA)
            {
                return Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA;
            }else if(gid == CustomerGroup.RICAMBISTA || gid == CustomerGroup.RICAMBISTA_A){
                return Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.RICAMBISTA;
            }else if(gid == CustomerGroup.GROSSISTA || gid == CustomerGroup.GROSSISTA_A){
                return Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.GROSSISTA;
            }else if(gid == CustomerGroup.OFF_S){
                return Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFF_S;
            }else{
                return Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.NORMALE;
            }
        }
    }
}
