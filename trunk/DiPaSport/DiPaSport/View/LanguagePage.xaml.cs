using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Com.IT.DiPaSport.Model;
using Com.IT.DiPaSport.Resources;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Threading;
using System.IO.IsolatedStorage;

namespace Com.IT.DiPaSport.View
{
    public partial class LanguagePage : PhoneApplicationPage
    {
        readonly BackgroundWorker backroungWorker;
        readonly Popup myPopup;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguagePage"/> class.
        /// </summary>
        public LanguagePage()
        {
            InitializeComponent();

            myPopup = new Popup()
            {
                IsOpen = true, Child = new SplashScreen()
            };
            backroungWorker = new BackgroundWorker();
            RunBackgroundWorker();
        }

        /// <summary>
        /// Runs the background worker.
        /// </summary>
        private void RunBackgroundWorker() 
        {
            backroungWorker.DoWork += ((s, args) => {
                Thread.Sleep(5000);
            });

            backroungWorker.RunWorkerCompleted += ((e, args) => {
                this.Dispatcher.BeginInvoke(() => { 
                    myPopup.IsOpen = false;
                    var setting = IsolatedStorageSettings.ApplicationSettings;
                    if (setting.Contains("language"))
                    {
                        NavigationService.Navigate(NavigationPages.MainPage);
                    }
                    else
                    {
                        LayoutRoot.Visibility = System.Windows.Visibility.Visible;
                    }
                });
            });

            backroungWorker.RunWorkerAsync();
        }

        private void language_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var setting = IsolatedStorageSettings.ApplicationSettings;
            if ((sender as Image).Name.Equals("lang_it"))
            {
                setting.Add("language", "it-IT");
                MessageBox.Show(AppResources.LanguageMsgIT);
            }
            else
            {
                setting.Add("language", "en-US");
                MessageBox.Show(AppResources.LanguageMsgUK);
            }
            setting.Save();
            NavigationService.Navigate(NavigationPages.MainPage);
        }
    }
}