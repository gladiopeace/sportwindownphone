using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class SearchModel : BaseModel
    {
        public void SearchByKeyword(string keyword, int page)
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

            string url = String.Format(Constants.URL_SEACH, new string[] { keyword, page.ToString(), userID });
            Execute(url);
        }

        public void SearchByCategory(string cat, int page)
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

            string url = String.Format(Constants.URL_SEARCH_BY_CATEGORY, new string[] { cat, page.ToString(), userID });
            Execute(url);
        }
    }
}
