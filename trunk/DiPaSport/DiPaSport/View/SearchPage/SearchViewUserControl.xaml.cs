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
using Newtonsoft.Json.Linq;
using Com.IT.DiPaSport.ViewModel;
using System.Collections.ObjectModel;
using Coding4Fun.Toolkit.Controls;
using Com.IT.DiPaSport.Resources;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View
{
    public partial class SearchViewUserControl : BaseUserControl, SearchBoxUserControl.OnSearchBoxListener
    {
        /// <summary>
        /// The keyword
        /// </summary>
        private string Keyword;
        /// <summary>
        /// The search action listener
        /// </summary>
        public SeachUserControl.OnSearchAction SearchActionListener;
        /// <summary>
        /// Gets or sets the search callback listener.
        /// </summary>
        /// <value>
        /// The search callback listener.
        /// </value>
        public JSONCallbackListener SearchCallbackListener { private get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewUserControl"/> class.
        /// </summary>
        public SearchViewUserControl()
        {
            InitializeComponent();
            Width = double.NaN; // Auto
            Height = Constants.Dimens.SCREEN_HEIGHT;
            Name = Constants.ScreensName.SEARCH;

            SearchBoxControl.SearchListener = this;
        }
        /// <summary>
        /// Called when [search].
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        public void OnSearch(string keyword)
        {
            if (keyword.Length == 0)
            {
                new MessagePrompt()
                {
                    Message = AppResources.SearchKeywordEmpty
                }.Show();
                SearchBoxControl.Focus();
                return;
            }
            this.Keyword = keyword;
            SearchBoxControl.Keyword = keyword;
            

            // Register callback when fetch json data
            ResultsControl.FetchJSONCallbackListener = SearchCallbackListener;

            // Register callback when user press button 'add to cart' or 'show details'
            ResultsControl.SearchActionListener = SearchActionListener;

            ResultsControl.OnClear();
            ResultsControl.OnSearch(keyword);
        }
    }
}
