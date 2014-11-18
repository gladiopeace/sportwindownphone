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
using System.IO.IsolatedStorage;
using Com.IT.DiPaSport.Resources;
using Coding4Fun.Toolkit.Controls;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View.AccountPage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LogoutViewUserControl : BaseUserControl
    {
        /// <summary>
        /// Gets or sets the account view listener.
        /// </summary>
        /// <value>
        /// The account view listener.
        /// </value>
        public AccountViewUserControl.AccountViewListener AccountListener { private get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutViewUserControl"/> class.
        /// </summary>
        public LogoutViewUserControl()
        {
            InitializeComponent();
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;
            Name = Constants.ScreensName.LOGOUT;

            var settings = IsolatedStorageSettings.ApplicationSettings;
            LoginObject loginObj = null;
            settings.TryGetValue<LoginObject>(Constants.JSON_TAG.LOGIN.ToString(), out loginObj);
            if (loginObj != null)
            {
                string welcome = String.Format("{0}, {1}", AppResources.Welcome, loginObj.UserName);
                LogoutWelcome.Text = welcome;
            }
        }

        /// <summary>
        /// Handles the Click event of the LogoutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessagePrompt confirm = new MessagePrompt()
            {
                Title = AppResources.LogoutConfirmTitle,
                Message = AppResources.LogoutConfirm,
                IsCancelVisible = true
            };

            confirm.Show();
            confirm.Completed += confirm_Completed;
        }

        /// <summary>
        /// Handles the Completed event of the confirm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PopUpEventArgs{System.String, PopUpResult}"/> instance containing the event data.</param>
        void confirm_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                LoginModel.Logout();

                // Notify to parent to change view
                if (AccountListener != null)
                {
                    AccountListener.OnLogout();
                }
            }
        }
    }
}
