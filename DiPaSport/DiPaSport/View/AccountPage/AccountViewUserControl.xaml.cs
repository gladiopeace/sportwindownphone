using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Com.IT.DiPaSport.Resources;
using Com.IT.DiPaSport.Model;
using Com.IT.DiPaSport.Model.Interfaces;
using Coding4Fun.Toolkit.Controls;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View
{
    public partial class AccountViewUserControl : BaseUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public interface AccountViewListener : OnRequestListener
        {
            /// <summary>
            /// Called when [login].
            /// </summary>
            void OnLogin();

            /// <summary>
            /// Called when [register].
            /// </summary>
            void OnRegister();

            /// <summary>
            /// Called when [lost password].
            /// </summary>
            void OnLostPassword();

            /// <summary>
            /// Called when [register back].
            /// </summary>
            void OnRegisterBack();

            /// <summary>
            /// Called when [logout].
            /// </summary>
            void OnLogout();

            #region CustomerType
            /// <summary>
            /// Called when [open customer type].
            /// </summary>
            void OnOpenCustomerType(int selectedIndex);
            /// <summary>
            /// Called when [selected customer type].
            /// </summary>
            /// <param name="cusType">Type of the cus.</param>
            /// <param name="index">The index.</param>
            void OnSelectedCustomerType(string cusType, int index);
            #endregion

            #region Country
            /// <summary>
            /// Called when [open countres].
            /// </summary>
            /// <param name="indexSelected">The index selected.</param>
            void OnOpenCountres(int selectedIndex);
            /// <summary>
            /// Called when [selected country].
            /// </summary>
            /// <param name="country">The country.</param>
            /// <param name="code">The code.</param>
            void OnSelectedCountry(CountryObject country, int selectedIndex);
            #endregion
        }

        /// <summary>
        /// Gets or sets the account view listener.
        /// </summary>
        /// <value>
        /// The account view listener.
        /// </value>
        public AccountViewListener AccountListener { private get; set; }

        /// <summary>
        /// Gets or sets the login callback.
        /// </summary>
        /// <value>
        /// The login callback.
        /// </value>
        public JSONCallbackListener LoginCallback { private get; set; }

        /// <summary>
        /// Gets or sets the lost password callback.
        /// </summary>
        /// <value>
        /// The lost password callback.
        /// </value>
        public JSONCallbackListener LostPasswordCallback { private get; set; }

        /// <summary>
        /// Gets or sets the loading.
        /// </summary>
        /// <value>
        /// The loading.
        /// </value>
        public OnRequestListener Loading { private get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountViewUserControl"/> class.
        /// </summary>
        public AccountViewUserControl()
        {
            InitializeComponent();
            Width = double.NaN; // Auto
            Height = Constants.Dimens.SCREEN_HEIGHT;
            Name = Constants.ScreensName.ACCOUNT;
#if DEBUG
            LoginName.Text = "marktestvn@gmail.com";
            LoginPass.Password = "123456";
#endif
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountListener != null)
            {
                AccountListener.OnRegister();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            AttemptLogin();   
        }

        private void LostPassword_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (AccountListener != null)
            {
                AccountListener.OnLostPassword();
            }

            InputPrompt lostPassConfirm = new InputPrompt()
            {
                Title = AppResources.LostPasswordTitle,
                IsCancelVisible = true,
                Message = AppResources.LostPasswordContent

            };
#if DEBUG
            lostPassConfirm.Value = "marktestvn@gmail.com";
#endif
            lostPassConfirm.Completed += lostPassConfirm_Completed;
            lostPassConfirm.Show();
        }

        void lostPassConfirm_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                LostPasswordModel lostPass = new LostPasswordModel() { RegisterCallback = LostPasswordCallback };
                lostPass.LostPassword(e.Result);
            }
        }

        /// <summary>
        /// Attempses the login.
        /// </summary>
        private void AttemptLogin()
        {
            if (LoginName.Text == String.Empty)
            {
                MessageBox.Show(AppResources.LoginNameEmpty);
                return;
            }

            if (LoginPass.Password == String.Empty)
            {
                MessageBox.Show(AppResources.LoginPasswordEmpty);
                return;
            }

            LoginModel login = new LoginModel() { RegisterCallback = LoginCallback };
            login.Login(LoginName.Text, LoginPass.Password);
        }
    }
}
