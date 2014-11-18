using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Com.IT.DiPaSport.Model.Interfaces;
using Com.IT.DiPaSport.View.BasePage;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace Com.IT.DiPaSport.View.SearchPage
{
    public partial class SearchNotFoundViewUserControl : BaseUserControl
    {
        /// <summary>
        /// Gets or sets the home action.
        /// </summary>
        /// <value>
        /// The home action.
        /// </value>
        public HomeActionListener HomeAction { private get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchNotFoundViewUserControl"/> class.
        /// </summary>
        public SearchNotFoundViewUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the SearchRequestSendButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SearchRequestSendButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Tap event of the RequestBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void RequestBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HomeAction != null)
            {
                HomeAction.OnBack();
            }
        }

        private void TakeRequest1_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask requestProduct = new CameraCaptureTask();
            requestProduct.Completed += requestProduct1_Completed;
            requestProduct.Show();
        }

        void requestProduct1_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                ImageRequest1.Source = bmp;
                ImageRequest1.Visibility = System.Windows.Visibility.Visible;
                TakeRequest1.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void TakeRequest2_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask requestProduct = new CameraCaptureTask();
            requestProduct.Completed += requestProduct2_Completed;
            requestProduct.Show();
        }

        void requestProduct2_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                ImageRequest2.Source = bmp;
                ImageRequest2.Visibility = System.Windows.Visibility.Visible;
                TakeRequest2.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void TakeRequest3_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask requestProduct = new CameraCaptureTask();
            requestProduct.Completed += requestProduct3_Completed;
            requestProduct.Show();
        }

        void requestProduct3_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                ImageRequest3.Source = bmp;
                ImageRequest3.Visibility = System.Windows.Visibility.Visible;
                TakeRequest3.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
