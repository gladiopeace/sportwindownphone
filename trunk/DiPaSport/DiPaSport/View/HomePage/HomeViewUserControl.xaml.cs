using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Com.IT.DiPaSport.Resources;
using Com.IT.DiPaSport.Model;
using System.Windows.Media.Imaging;
using Com.IT.DiPaSport.Model.Interfaces;
using Com.IT.DiPaSport.View.BasePage;
using Coding4Fun.Toolkit.Controls;
using Newtonsoft.Json.Linq;
using Com.IT.DiPaSport.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Resources;
using System.IO.IsolatedStorage;

namespace Com.IT.DiPaSport.View
{
    public partial class HomeViewUserControl : BaseUserControl, SearchBoxUserControl.OnSearchBoxListener
    {
        /// <summary>
        /// Gets or sets the home listener.
        /// </summary>
        /// <value>
        /// The home listener.
        /// </value>
        public HomeActionListener HomeListener { private get; set; }

        /// <summary>
        /// The product feature
        /// </summary>
        private ProductFeaturesModel ProductFeature;

        /// <summary>
        /// The thiz instance
        /// </summary>
        private static HomeViewUserControl ThizInstance;

        /// <summary>
        /// Gets or sets the search action listener.
        /// </summary>
        /// <value>
        /// The search action listener.
        /// </value>
        public SeachUserControl.OnSearchAction DetailActionListener { private get; set; }

        private IList<Image> ListProductsImage;
        private IList<TextBlock> ListProductsName;
        private ObservableCollection<ProductTable> ProductsFeature;
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewUserControl"/> class.
        /// </summary>
        public HomeViewUserControl()
        {
            InitializeComponent();
            Width = double.NaN; // Auto
            Height = Constants.Dimens.SCREEN_HEIGHT;
            Name = Constants.ScreensName.HOME;

            SearchBoxControl.SearchListener = this;
            ProductFeature = new ProductFeaturesModel();
            //Loaded += HomeViewUserControl_Loaded;

            ListProductsImage = new List<Image>();
            ListProductsName = new List<TextBlock>();

            ListProductsImage.Add(Product1Image);
            ListProductsImage.Add(Product2Image);
            ListProductsImage.Add(Product3Image);
            ListProductsImage.Add(Product4Image);

            ListProductsName.Add(Product1Name);
            ListProductsName.Add(Product2Name);
            ListProductsName.Add(Product3Name);
            ListProductsName.Add(Product4Name);
            ThizInstance = this;

            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("language"))
            {
                string language = (string)settings["language"];
                if (language.Equals("it-IT"))
                {
                    BitmapImage bmp = new BitmapImage(new Uri("/Asserts/Images/loader.it.png"));
                    Product1Image.Source = bmp;
                    Product2Image.Source = bmp;
                    Product3Image.Source = bmp;
                    Product4Image.Source = bmp;
                }
            }
        }

        /// <summary>
        /// Called when [load].
        /// </summary>
        public override void OnLoad()
        {
            ProductFeature.RegisterCallback = new ProductFeaturesCallback();
            ProductFeature.ProductFeatures();
        }

        /// <summary>
        /// Handles the Tap event of the SearchByCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void SearchByCategory_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HomeListener != null)
            {
                HomeListener.OnOpenCategory();
            }
        }

        /// <summary>
        /// Handles the Tap event of the CallUs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void CallUs_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhoneCallTask callus = new PhoneCallTask()
            {
                PhoneNumber = "+390523509862",
                DisplayName = AppResources.AppName
            };
            callus.Show();
        }

        /// <summary>
        /// Called when [search].
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        public void OnSearch(string keyword)
        {
            if (HomeListener != null)
            {
                HomeListener.OnSearch(keyword);
            }
        }

        /// <summary>
        /// Handles the Tap event of the RequestProduct control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void RequestProduct_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HomeListener != null)
            {
                HomeListener.OnRequestProduct();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class ProductFeaturesCallback : JSONCallbackListener
        {
            public override void OnResults(JObject results)
            {
                SearchViewModel.Convert(results);
                ThizInstance.ProductsFeature = SearchViewModel.Results;
                if ((int)results[Constants.JSON_TAG.ERROR_CODE] == 0)
                {
                    for (int i = 0; i < ThizInstance.ProductsFeature.Count; i++)
                    {
                        Uri ImageUri = new Uri(ThizInstance.ProductsFeature[i].ImagePath);
                        ThizInstance.ListProductsImage[i].Source = new BitmapImage(ImageUri);
                        ThizInstance.ListProductsName[i].Text = ThizInstance.ProductsFeature[i].Code;
                    }
                }
                else
                {
                    new ToastPrompt()
                    {
                        Message = AppResources.ServerNotResponse
                    }.Show();
                }
            }

            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                new ToastPrompt()
                {
                    Message = AppResources.ServerNotResponse
                }.Show();
            }

            public override void StartRequest()
            {
                if (ThizInstance.HomeListener != null)
                {
                    ThizInstance.HomeListener.StartRequest();
                }
            }

            public override void EndRequest()
            {
                if (ThizInstance.HomeListener != null)
                {
                    ThizInstance.HomeListener.EndRequest();
                }
            }
        }

        private void Product1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (DetailActionListener != null)
            {
                if (ProductsFeature != null)
                    DetailActionListener.OnOpenDetail(ProductsFeature[0]);
            }
        }

        private void Product2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (DetailActionListener != null)
            {
                if (ProductsFeature != null)
                    DetailActionListener.OnOpenDetail(ProductsFeature[1]);
            }
        }

        private void Product3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (DetailActionListener != null)
            {
                if (ProductsFeature != null)
                    DetailActionListener.OnOpenDetail(ProductsFeature[2]);
            }
        }

        private void Product4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (DetailActionListener != null)
            {
                if (ProductsFeature != null)
                    DetailActionListener.OnOpenDetail(ProductsFeature[3]);
            }
        }
    }
}
