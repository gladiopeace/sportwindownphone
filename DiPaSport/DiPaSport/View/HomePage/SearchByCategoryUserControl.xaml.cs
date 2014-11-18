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
using System.Collections.Specialized;
using Com.IT.DiPaSport.Model.Interfaces;
using Com.IT.DiPaSport.Resources;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View
{
    public partial class SearchByCategoryUserControl : BaseUserControl, CategoryViewModel.CategoryListener
    {
        /// <summary>
        /// The search action listener
        /// </summary>
        public SeachUserControl.OnSearchAction SearchActionListener;
        /// <summary>
        /// The category manager view model
        /// </summary>
        private readonly CategoryViewModel CategoryManagerViewModel;

        /// <summary>
        /// Gets or sets the product view callback.
        /// </summary>
        /// <value>
        /// The product view callback.
        /// </value>
        public OnSearchByCategoryListener SearchByCategoryListener { private get; set; }

        /// <summary>
        /// Gets or sets the home listener.
        /// </summary>
        /// <value>
        /// The home listener.
        /// </value>
        public HomeActionListener HomeListener { private get; set; }

        /// <summary>
        /// Gets or sets the search callback listener.
        /// </summary>
        /// <value>
        /// The search callback listener.
        /// </value>
        public JSONCallbackListener FetchJSONCallbackListener { private get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchByCategoryUserControl"/> class.
        /// </summary>
        public SearchByCategoryUserControl()
        {
            InitializeComponent();
            Name = Constants.ScreensName.SEARCH;
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;

            Loaded += SearchByCategoryUserControl_Loaded;
            CategoryManagerViewModel = new CategoryViewModel();
            ResultControl.IsSeachByCategory = true;
        }

        /// <summary>
        /// Handles the Loaded event of the SearchByCategoryUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void SearchByCategoryUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CategoryManagerViewModel.CategoryCallback = this;
            CategoryManagerViewModel.HomeListener = HomeListener;
            CategoryManagerViewModel.fetchListCategories();
        }

        /// <summary>
        /// Called when [fetch category].
        /// </summary>
        /// <param name="categories">The categories.</param>
        public void OnFetchCategory(System.Collections.ObjectModel.ObservableCollection<CategoryInfo> categories)
        {
            MainCategory.DataContext = categories;
        }

        /// <summary>
        /// Called when [fetch error].
        /// </summary>
        public void OnFetchError()
        {
            MessageBox.Show(AppResources.NoNetwork);
        }

        /// <summary>
        /// Back to main category
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void CatBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (MainCategory.Visibility == System.Windows.Visibility.Visible)
            {
                if (HomeListener != null)
                {
                    HomeListener.OnCloseCategory();
                }
            }
            else if (ResultControl.Visibility == System.Windows.Visibility.Visible)
            {
                ResultControl.Visibility = System.Windows.Visibility.Collapsed;
                SubCategory.Visibility = System.Windows.Visibility.Visible;
                ResultControl.OnClear();
            }
            else
            {
                MainCategory.Visibility = Visibility.Visible;
                SubCategory.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Handles the Tap event of the CatItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void CatItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            CategoryInfo cat = (CategoryInfo)MainCategory.SelectedItem;
            MainCategory.SelectedIndex = -1;
            if (cat != null)
            {
                if (cat.SubCategories != null)
                {
                    SubCategory.DataContext = cat.SubCategories;
                    MainCategory.Visibility = Visibility.Collapsed;
                    SubCategory.Visibility = Visibility.Visible;
                    CatBack.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Handles the Tap event of the SubCatItem control. Notify to main page when need search by categoryManagerViewModel
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void SubCatItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Search products by category
            

                CategoryInfo subCat = (CategoryInfo)SubCategory.SelectedItem;
                if (subCat != null)
                {
                    //TODO: Need register listener when search by category
                    //SearchByCategoryListener.OnSearchByCategory(subCat.ID);
                    ResultControl.FetchJSONCallbackListener = FetchJSONCallbackListener;
                    ResultControl.SearchActionListener = SearchActionListener;
                    ResultControl.OnSearchWithCategory(subCat.ID);
                    ResultControl.Visibility = System.Windows.Visibility.Visible;
                    MainCategory.Visibility = Visibility.Collapsed;
                }
            SubCategory.SelectedIndex = -1;
        }
    }
}
