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
using Com.IT.DiPaSport.Model.Interfaces;
using Microsoft.Phone.Tasks;
using Com.IT.DiPaSport.Resources;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View
{
    public partial class ContactViewUserControl : BaseUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public interface ContactViewListener : DiPaListener, OnRequestListener
        {
            void OnOpenFacebook();
            void OnOpenTwitter();
            void OnOpenYoutube();
            void OnOpenMap();
        }

        /// <summary>
        /// Gets or sets the contact listener.
        /// </summary>
        /// <value>
        /// The contact listener.
        /// </value>
        public ContactViewListener ContactListener { private get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactViewUserControl"/> class.
        /// </summary>
        public ContactViewUserControl()
        {
            InitializeComponent();
            Width = double.NaN; // Auto
            Height = Constants.Dimens.SCREEN_HEIGHT;
            Name = Constants.ScreensName.CONTACT;
        }

        private void CallUs_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhoneCallTask callus = new PhoneCallTask()
            {
                PhoneNumber = "+390523509862",
                DisplayName = AppResources.AppName
            };
            callus.Show();
        }

        private void EmailUs_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask contact = new EmailComposeTask()
            {
                To= Constants.EMAIL_INFO
            };
            contact.Show();
        }

        private void Maps_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (ContactListener != null)
            {
                ContactListener.OnOpenMap();
            }
        }

        private void Facebook_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (ContactListener != null)
            {
                ContactListener.OnOpenFacebook();
            }
        }

        private void Twitter_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (ContactListener != null)
            {
                ContactListener.OnOpenTwitter();
            }
        }

        private void Youtube_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (ContactListener != null)
            {
                ContactListener.OnOpenYoutube();
            }
        }
    }
}
