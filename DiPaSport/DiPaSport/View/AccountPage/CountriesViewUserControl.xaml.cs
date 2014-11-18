using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Globalization;
using DiPaSport;
using Com.IT.DiPaSport.Model;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View.AccountPage
{
    public partial class CountriesViewUserControl : BaseUserControl
    {
        /// <summary>
        /// The account listener
        /// </summary>
        public AccountViewUserControl.AccountViewListener AccountListener;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesViewUserControl"/> class.
        /// </summary>
        public CountriesViewUserControl()
        {
            InitializeComponent();
            Name = Constants.ScreensName.COUNTRIES;
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;

            loadCountries();
        }

        private void Country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountListener != null)
            {
                CountryObject country = ListOfCountries.SelectedItem as CountryObject;
                int selectedIndex = ListOfCountries.SelectedIndex;
                AccountListener.OnSelectedCountry(country, selectedIndex);
            }
        }

        public void loadCountries()
        {
            List<CountryObject> countries = new List<CountryObject>() { 
                    new CountryObject(){
                        Name = "Italia",
                        Code = "IT"
                    },
                    new CountryObject(){
                        Name = "English",
                        Code = "UK"
                    }
                };
            if (App.CurrentLanguage == Model.Languages.IT)
            {
                
                ListOfCountries.DataContext = countries;
            }
            else
            {
                ListOfCountries.DataContext = countries;
            }
        }
    }
}
