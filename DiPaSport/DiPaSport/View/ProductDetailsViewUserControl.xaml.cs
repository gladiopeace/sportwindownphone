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
using Com.IT.DiPaSport.Model;
using Coding4Fun.Toolkit.Controls;
using Com.IT.DiPaSport.Resources;
using Com.IT.DiPaSport.Model.Customer;

namespace Com.IT.DiPaSport.View
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProductDetailsViewUserControl : BaseUserControl
    {
        /// <summary>
        /// The current product
        /// </summary>
        private ProductTable CurrentProduct;

        /// <summary>
        /// The c urrent product details
        /// </summary>
        private ProductInfo CurrentProductDetails;
        /// <summary>
        /// Gets or sets the home listener.
        /// </summary>
        /// <value>
        /// The home listener.
        /// </value>
        public HomeActionListener HomeListener { private get; set; }

        /// <summary>
        /// Gets or sets the search action listener.
        /// </summary>
        /// <value>
        /// The search action listener.
        /// </value>
        public SeachUserControl.OnSearchAction SearchActionListener { private get; set; }
        
        /// <summary>
        /// The thiz instance
        /// </summary>
        private static ProductDetailsViewUserControl ThizInstance;

        /// <summary>
        /// The product detail
        /// </summary>
        private ProductDetailsModel productDetail;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailsViewUserControl"/> class.
        /// </summary>
        public ProductDetailsViewUserControl()
        {
            InitializeComponent();
            Name = Constants.ScreensName.DETAIL;
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;

            ThizInstance = this;
            productDetail = new ProductDetailsModel();
        }

        public ProductDetailsViewUserControl(ProductTable p) : this()
        {
            CurrentProduct = p;
        }

        /// <summary>
        /// Called when [load].
        /// </summary>
        public override void OnLoad()
        {
            productDetail.RegisterCallback = new FetchProductDetail();
            productDetail.FetchProductDetails(CurrentProduct.ID);
            if (!LoginModel.IsLogin)
            {
                MessagePrompt confirmLogin = new MessagePrompt()
                {
                    Message = AppResources.DetailAlertLogin,
                    IsCancelVisible = true
                };
                confirmLogin.Completed += confirmLogin_Completed;
                confirmLogin.Show();
            }
        }

        void confirmLogin_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                if (HomeListener != null)
                {
                    HomeListener.OnLoginRequired();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class FetchProductDetail : JSONCallbackListener
        {
            /// <summary>
            /// Called when download and convert to JObject successful.
            /// </summary>
            /// <param name="results">The results.</param>
            public override void OnResults(Newtonsoft.Json.Linq.JObject results)
            {
                ThizInstance.CurrentProductDetails = ProductDetailsModel.GetProductDetails(results);
                ThizInstance.CurrentProduct.SpecialPrice = ThizInstance.CurrentProductDetails.SpecialPrice;
                ThizInstance.ContentPanel.DataContext = ThizInstance.CurrentProductDetails;

                bool g0 = LoginModel.Group == CustomerGroup.NOT_LOGGED_IN;
                bool g16 = LoginModel.Group == CustomerGroup.NOPREZZI;
                bool g17 = LoginModel.Group == CustomerGroup.BANNATI;
                if (!LoginModel.IsLogin || (g0 || g16 || g17))
                {
                    ThizInstance.AddToCart.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ThizInstance.AddToCart.Visibility = Visibility.Visible;
                }
            }

            /// <summary>
            /// Call when errors related connection or convert to JObject fail.
            /// </summary>
            /// <param name="ErrorCode">The error code.</param>
            /// <param name="ErrorMessage">The error message.</param>
            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                new ToastPrompt()
                {
                    Message = AppResources.ServerNotResponse
                }.Show();
            }

            /// <summary>
            /// Begins the loading.
            /// </summary>
            public override void StartRequest()
            {
                if (ThizInstance.HomeListener != null)
                {
                    ThizInstance.HomeListener.StartRequest();
                }
            }

            /// <summary>
            /// Ends the loading.
            /// </summary>
            public override void EndRequest()
            {
                if (ThizInstance.HomeListener != null)
                {
                    ThizInstance.HomeListener.EndRequest();
                }
            }
        }

        private void DetailBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (SearchActionListener != null)
            {
                SearchActionListener.OnCloseDetail();
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (SearchActionListener != null)
            {
                SearchActionListener.OnOpenQuantity(CurrentProduct);
            }
        }

        private void SlideImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (SearchActionListener != null)
            {
                SearchActionListener.OnOpenSlideImage(CurrentProductDetails.Images, CurrentProductDetails.Name);
            }
        }
    }
}
