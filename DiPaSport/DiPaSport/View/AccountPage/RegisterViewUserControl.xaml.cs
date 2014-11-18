using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Com.IT.DiPaSport.Model;
using System.Globalization;
using Coding4Fun.Toolkit.Controls;
using Com.IT.DiPaSport.Resources;
using DiPaSport;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View
{
    public partial class RegisterViewUserControl : BaseUserControl
    {
        private static RegisterViewUserControl ThizInstance;
        /// <summary>
        /// Gets or sets the account view listener.
        /// </summary>
        /// <value>
        /// The account view listener.
        /// </value>
        public AccountViewUserControl.AccountViewListener AccountListener { private get; set; }

        /// <summary>
        /// Gets or sets the register callback listener.
        /// </summary>
        /// <value>
        /// The register callback listener.
        /// </value>
        public JSONCallbackListener RegisterCallbackListener { private get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterViewUserControl"/> class.
        /// </summary>
        public RegisterViewUserControl()
        {
            InitializeComponent();
            Name = Constants.ScreensName.REGISTER;
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;

            coutries();
            loadDebugData();

            ThizInstance = this;
        }

        /// <summary>
        /// Loads the debug data.
        /// </summary>
        private void loadDebugData()
        {
#if DEBUG
            RegFirstName.Text = "van";
            RegLastName.Text = "vo";
            RegCompany.Text = "my company";
            RegTax.Text = "123456";
            RegAddress.Text = "my address";
            RegCity.Text = "my city";
            RegState.Text = "my state";
            RegZIP.Text = "my zip";
            RegTelephone.Text = "123456789";
            RegFax.Text = "654321";
            RegEmail.Text = "test001@gmail.com";
            RegPass.Password = "123456";
            RegPassConfirm.Password = "123456";
#endif
        }

        private void RegBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (AccountListener != null)
            {
                AccountListener.OnRegisterBack();
            }
        }

        private void PrivacyLink_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            new  WebBrowserTask(){
                Uri = new Uri(AppResources.RegisterPrivacyUrl, UriKind.Absolute)
            }.Show();
        }

        private void RegCancel_Click(object sender, RoutedEventArgs e)
        {
            if (AccountListener != null)
            {
                AccountListener.OnRegisterBack();
            }
        }

        private void RegOK_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                // registration
                bool privacy = RegPrivacy.IsChecked == true ? true : false;

                if (!privacy)
                {
                    new MessagePrompt()
                    {
                        Message = "Selezionare una opzione."
                    }.Show();
                }
                else
                {
                    RegisterModel reg = new RegisterModel() { RegisterCallback = RegisterCallbackListener };
                    reg.Register(userInfo());
                }
            }
        }

        private void coutries()
        {
            
        }

        /// <summary>
        /// Handles the Tap event of the CustomerType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void CustomerType_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (AccountListener != null)
            {
                int selectedIndex = int.Parse(CustomerType.Tag.ToString());
                AccountListener.OnOpenCustomerType(selectedIndex);
            }
        }

        /// <summary>
        /// Updates the type of the customer.
        /// </summary>
        /// <param name="customerType">Type of the customer.</param>
        /// <param name="index">The index.</param>
        public void UpdateCustomerType(string customerType, int index)
        {
            CustomerType.Text = customerType;
            CustomerType.Tag = index;
        }

        /// <summary>
        /// Updates the country.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="index">The index.</param>
        public void UpdateCountry(string country, int index)
        {
            Country.Text = country;
            Country.Tag = index;
        }

        /// <summary>
        /// Handles the Tap event of the Country control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void Country_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (AccountListener != null)
            {
                int selectedIndex = int.Parse(Country.Tag.ToString());
                AccountListener.OnOpenCountres(selectedIndex);
            }
        }

        private bool Validate()
        {
            bool flag = true;

            if (CustomerType.Tag.ToString().Equals("-1"))
            {
                CustomerType.Foreground = new SolidColorBrush(Colors.Red);
                flag = false;
            }
            else
            {
                CustomerType.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (RegFirstName.Text == String.Empty)
            {
                RegFirstName.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegFirstName.IsHighLight = false;
            }

            if (RegLastName.Text == String.Empty)
            {
                RegLastName.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegLastName.IsHighLight = false;
            }

            if (RegCompany.Text == String.Empty)
            {
                RegCompany.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegCompany.IsHighLight = false;
            }

            if (RegTax.Text == String.Empty)
            {
                RegTax.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegTax.IsHighLight = false;
            }

            if (RegAddress.Text == String.Empty)
            {
                RegAddress.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegAddress.IsHighLight = false;
            }

            if (RegCity.Text == String.Empty)
            {
                RegCity.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegCity.IsHighLight = false;
            }

            if (RegZIP.Text == String.Empty)
            {
                RegZIP.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegZIP.IsHighLight = false;
            }

            if (Country.Tag.ToString().Equals("-1"))
            {
                Country.Foreground = new SolidColorBrush(Colors.Red);
                flag = false;
            }
            else
            {
                Country.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (RegTelephone.Text == String.Empty)
            {
                RegTelephone.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegTelephone.IsHighLight = false;
            }

            if (RegEmail.Text == String.Empty)
            {
                RegEmail.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegEmail.IsHighLight = false;
            }

            if (RegPass.Password == String.Empty)
            {
                RegPass.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegPass.IsHighLight = false;
            }

            if (RegPassConfirm.Password == String.Empty)
            {
                RegPassConfirm.IsHighLight = true;
                flag = false;
            }
            else
            {
                RegPassConfirm.IsHighLight = false;
            }

            if (!flag)
            {
                new MessagePrompt()
                {
                    Message = AppResources.RequestInputRequire
                }.Show();
            }
            else {
                // Check password match
                if (RegPass.Password.Length + RegPassConfirm.Password.Length > 1 && !RegPass.Password.Equals(RegPassConfirm.Password))
                {
                    new MessagePrompt()
                    {
                        Message = AppResources.RegisterPasswordNotMatch
                    }.Show();
                    RegPass.IsHighLight = true;
                    RegPassConfirm.IsHighLight = true;
                    return false;
                }
                else
                {
                    RegPass.IsHighLight = false;
                    RegPassConfirm.IsHighLight = false;    
                }

                // Check password length
                if (RegPass.Password.Length < 6 && RegPassConfirm.Password.Length < 6)
                {
                    new MessagePrompt()
                    {
                        Message = AppResources.RegisterPasswordMinLength
                    }.Show();
                    RegPass.IsHighLight = true;
                    RegPassConfirm.IsHighLight = true;
                    return false;
                }
                else
                {
                    RegPass.IsHighLight = false;
                    RegPassConfirm.IsHighLight = false;
                }
            }

            return flag;
        }

        private Dictionary<string, string> userInfo()
        {
            Dictionary<string, string> user = new Dictionary<string, string>();
            user.Add("custype", CustomerType.Text);
            user.Add("fname", RegFirstName.Text);
            user.Add("lname", RegLastName.Text);
            user.Add("email", RegEmail.Text);
            user.Add("pass", RegPass.Password);
            user.Add("company", RegCompany.Text);
            user.Add("tax", RegTax.Text);
            user.Add("address", RegAddress.Text);
            user.Add("city", RegCity.Text);
            user.Add("region", RegState.Text);
            user.Add("postcode", RegZIP.Text);
            user.Add("country", Country.Text);
            user.Add("tel", RegTelephone.Text);
            user.Add("fax", RegFax.Text);
            return user;
        }

    }
}
