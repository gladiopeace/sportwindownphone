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
using Com.IT.DiPaSport.ViewModel;
using Newtonsoft.Json.Linq;
using Coding4Fun.Toolkit.Controls;
using Com.IT.DiPaSport.Resources;
using Com.IT.DiPaSport.View.CartPage;
using Com.IT.DiPaSport.View.BasePage;
using System.Windows.Media.Imaging;

namespace Com.IT.DiPaSport.View
{
    public partial class SeachUserControl : BaseUserControl
    {

        /// <summary>
        /// 
        /// </summary>
        public interface OnSearchAction
        {
            void OnOpenQuantity(ProductTable p);
            void OnCloseQuantity();

            void OnOpenDetail(ProductTable p);
            void OnOpenSlideImage(List<string> images, string name);
            void OnCloseDetail();
        }

        /// <summary>
        /// Gets or sets the search action listener.
        /// </summary>
        /// <value>
        /// The search action listener.
        /// </value>
        public OnSearchAction SearchActionListener { private get; set; }
        /// <summary>
        /// Gets or sets the search callback listener.
        /// </summary>
        /// <value>
        /// The search callback listener.
        /// </value>
        public JSONCallbackListener FetchJSONCallbackListener { private get; set; }

        /// <summary>
        /// The on quantity changed
        /// </summary>
        public CartQuantityViewUserControl.OnQuantityChangedListener OnQuantityChanged;

        /// <summary>
        /// The search
        /// </summary>
        SearchModel Search;

        /// <summary>
        /// The page
        /// </summary>
        private int Page = 1;
        private string Keyword = String.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is seach by category.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is seach by category; otherwise, <c>false</c>.
        /// </value>
        public bool IsSeachByCategory { private get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeachUserControl"/> class.
        /// </summary>
        public SeachUserControl()
        {
            InitializeComponent();

            Search = new SearchModel()
            {
                RegisterCallback = new OnSearchListener(this)
            };
        }

        public void OnSearch(string keyword)
        {
            Keyword = keyword;
            Search.SearchByKeyword(Keyword, Page);
        }

        public void OnSearchWithCategory(string category)
        {
            Keyword = category;
            Search.SearchByCategory(Keyword, Page);
        }

        public void OnClear()
        {
            Page = 1;
            SearchResultsList.Items.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private class OnSearchListener : JSONCallbackListener
        {
            private readonly SeachUserControl ThizInstance;
            public OnSearchListener(SeachUserControl thiz)
            {
                ThizInstance = thiz;
            }

            /// <summary>
            /// Called when download and convert to JObject successful.
            /// </summary>
            /// <param name="results">The results.</param>
            public override void OnResults(JObject results)
            {
                SearchViewModel.Convert(results);
                foreach (var p in SearchViewModel.Results)
                {
                    ThizInstance.SearchResultsList.Items.Add(p);
                }

                if (ThizInstance.FetchJSONCallbackListener != null)
                {
                    ThizInstance.FetchJSONCallbackListener.OnResults(results);
                }
            }

            /// <summary>
            /// Call when errors related connection or convert to JObject fail.
            /// </summary>
            /// <param name="ErrorCode">The error code.</param>
            /// <param name="ErrorMessage">The error message.</param>
            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                if (ThizInstance.FetchJSONCallbackListener != null)
                {
                    ThizInstance.FetchJSONCallbackListener.OnErrors(ErrorCode, ErrorMessage);
                }
            }

            /// <summary>
            /// Begins the loading.
            /// </summary>
            public override void StartRequest()
            {
                if (ThizInstance.FetchJSONCallbackListener != null)
                {
                    ThizInstance.FetchJSONCallbackListener.StartRequest();
                }
            }

            /// <summary>
            /// Ends the loading.
            /// </summary>
            public override void EndRequest()
            {
                if (ThizInstance.FetchJSONCallbackListener != null)
                {
                    ThizInstance.FetchJSONCallbackListener.EndRequest();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the LoadMore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void LoadMore_Click(object sender, RoutedEventArgs e)
        {
            if (SearchViewModel.IsNextPage)
            {
                if (IsSeachByCategory)
                {
                    Search.SearchByCategory(Keyword, ++Page);
                }
                else
                {
                    Search.SearchByKeyword(Keyword, ++Page);
                }
            }
            else
            {
                new MessagePrompt()
                {
                    Message = AppResources.SearchNotFound
                }.Show();
            }
        }

        /// <summary>
        /// Handles the Click event of the AddToCartButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchActionListener != null)
            {
                string entityId = (sender as Button).Tag.ToString();
                foreach (ProductTable p in SearchResultsList.Items)
                {
                    if (p.ID == entityId)
                    {
                        SearchActionListener.OnOpenQuantity(p);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ShowDetailsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ShowDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchActionListener != null)
            {
                string entityId = (sender as Button).Tag.ToString();
                foreach (ProductTable p in SearchResultsList.Items)
                {
                    if (p.ID == entityId)
                    {
                        SearchActionListener.OnOpenDetail(p);
                        break;
                    }
                }
            }
        }

        // Load default image
        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Uri uri = new Uri("/Asserts/Images/logo.png", UriKind.RelativeOrAbsolute);
            (sender as Image).Source = new BitmapImage(uri);
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (SearchActionListener != null)
            {
                string entityId = (sender as Image).Tag.ToString();
                foreach (ProductTable p in SearchResultsList.Items)
                {
                    if (p.ID == entityId)
                    {
                        SearchActionListener.OnOpenDetail(p);
                        break;
                    }
                }
            }
        }

        private void PName_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (SearchActionListener != null)
            {
                string entityId = (sender as TextBlock).Tag.ToString();
                foreach (ProductTable p in SearchResultsList.Items)
                {
                    if (p.ID == entityId)
                    {
                        SearchActionListener.OnOpenDetail(p);
                        break;
                    }
                }
            }
        }
    }
}
