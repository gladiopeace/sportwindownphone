using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Com.IT.DiPaSport.View.BasePage;
using Com.IT.DiPaSport.Model.Interfaces;
using System.Windows.Media.Imaging;

namespace Com.IT.DiPaSport.View
{
    public partial class SlideImageViewUserControl : BaseUserControl
    {
        /// <summary>
        /// Gets or sets the home action.
        /// </summary>
        /// <value>
        /// The home action.
        /// </value>
        public HomeActionListener HomeAction { private get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public List<string> Images { private get; set; }

        /// <summary>
        /// Gets or sets the identifying name of the object. When a XAML processor creates the object tree from XAML markup, run-time code can refer to the XAML-declared object by this name.
        /// </summary>
        public string ProductName { private get; set; }


        /// <summary>
        /// The current image index
        /// </summary>
        private int CurrentImageIndex = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideImageViewUserControl"/> class.
        /// </summary>
        public SlideImageViewUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when [load].
        /// </summary>
        public override void OnLoad()
        {
            if (ProductName != null)
            {
                Name.Text = ProductName;
            }

            if (Images != null && Images.Count > 0)
            {
                CountImage.Text = string.Format("{0}/{1}", CurrentImageIndex + 1, Images.Count);
            }

            if (Images.Count == 1)
            {
                Prev.Visibility = Next.Visibility = System.Windows.Visibility.Collapsed;
            }

            Prev.IsEnabled = false;

            Image.Source = new BitmapImage(new Uri(Images[0]));
        }

        /// <summary>
        /// Handles the Click event of the Prev control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            --CurrentImageIndex;
            if (CurrentImageIndex == 0)
            {
                Prev.IsEnabled = false;// = System.Windows.Visibility.Collapsed;
            }

            Image.Source = new BitmapImage(new Uri(Images[CurrentImageIndex]));
            Next.IsEnabled = true;
            CountImage.Text = string.Format("{0}/{1}", CurrentImageIndex + 1, Images.Count);
        }

        /// <summary>
        /// Handles the Click event of the Next control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            ++CurrentImageIndex;
            if (CurrentImageIndex == Images.Count - 1)
            {
                Next.IsEnabled = false;// = System.Windows.Visibility.Collapsed;
            }

            Image.Source = new BitmapImage(new Uri(Images[CurrentImageIndex]));
            Prev.IsEnabled = true;
            CountImage.Text = string.Format("{0}/{1}", CurrentImageIndex + 1, Images.Count);
        }

        private void SlideBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HomeAction != null)
            {
                HomeAction.OnBack();
            }
        }
    }
}
