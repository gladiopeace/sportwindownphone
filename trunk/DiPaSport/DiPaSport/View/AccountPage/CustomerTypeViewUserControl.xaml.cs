using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DiPaSport;
using Com.IT.DiPaSport.Model;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View.AccountPage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CustomerTypeViewUserControl : BaseUserControl
    {
        /// <summary>
        /// The account listener
        /// </summary>
        public AccountViewUserControl.AccountViewListener AccountListener;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerTypeViewUserControl"/> class.
        /// </summary>
        public CustomerTypeViewUserControl()
        {
            InitializeComponent();

            Name = Constants.ScreensName.CUSTOMER_TYPE;
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;

            loadCustomerType();
        }

        /// <summary>
        /// Loads the type of the customer.
        /// </summary>
        private void loadCustomerType()
        {
            if (App.CurrentLanguage == Model.Languages.IT)
            {
                CustomerType.DataContext = App.CustomerTypeIT;
            }
            else
            {
                CustomerType.DataContext = App.CustomerTypeEN;
            }
            
        }

        /// <summary>
        /// Handles the SelectionChanged event of the CustomerType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void CustomerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountListener != null)
            {
                string customerType = CustomerType.SelectedItem as string;
                int selectedIndex = CustomerType.SelectedIndex;
                AccountListener.OnSelectedCustomerType(customerType, selectedIndex);
            }
        }
    }
}
