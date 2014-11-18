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
using System.Diagnostics;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View.ContactPage
{
    public partial class SocialViewUserControl : BaseUserControl
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string URL { private get; set; }
        
        /// <summary>
        /// [ERROR: Unknown property access] the contact listener.
        /// </summary>
        /// <value>
        /// The contact listener.
        /// </value>
        public ContactViewUserControl.ContactViewListener ContactListener { private get; set; }

        public SocialViewUserControl()
        {
            InitializeComponent();
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;
            Name = Constants.ScreensName.SOCIAL;
            SocialBrowser.Loaded += SocialBrowser_Loaded;
            SocialBrowser.LoadCompleted += SocialBrowser_LoadCompleted;
            SocialBrowser.Navigated += SocialBrowser_Navigated;
            SocialBrowser.NavigationFailed += SocialBrowser_NavigationFailed;
        }

        void SocialBrowser_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
#if DEBUG
            Debug.WriteLine("SocialBrowser_NavigationFailed");
#endif
            if (ContactListener != null)
            {
                ContactListener.EndRequest();
            }
        }

        void SocialBrowser_Navigated(object sender, NavigationEventArgs e)
        {
#if DEBUG
            Debug.WriteLine("SocialBrowser_Navigated");
#endif
            if (ContactListener != null)
            {
                ContactListener.EndRequest();
            }
        }

        void SocialBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
#if DEBUG
            Debug.WriteLine("SocialBrowser_LoadCompleted");
#endif
            
        }

        /// <summary>
        /// Handles the Loaded event of the SocialBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void SocialBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            if (ContactListener != null)
            {
                ContactListener.StartRequest();
            }
            SocialBrowser.Navigate(new Uri(URL, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Handles the Tap event of the SocialBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void SocialBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if(ContactListener != null)
            {
                ContactListener.OnBack();
            }
        }
    }
}
