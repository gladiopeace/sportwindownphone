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
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View
{
    public partial class SearchBoxUserControl : UserControl
    {
        /// <summary>
        /// Gets or sets the search listener.
        /// </summary>
        /// <value>
        /// The search listener.
        /// </value>
        public OnSearchBoxListener SearchListener { private get; set; }

        /// <summary>
        /// 
        /// </summary>
        public interface OnSearchBoxListener
        {
            void OnSearch(string keyword);
        }


        /// <summary>
        /// The keyword property
        /// </summary>
        public static DependencyProperty KeywordProperty = DependencyProperty.Register("Keyword", typeof(string), typeof(SearchBoxUserControl), null);

        public string Keyword
        {
            get
            {
                return (string)GetValue(KeywordProperty);
            }
            set
            {
                SetValue(KeywordProperty, value);
                SearchKeyword.Text = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBoxUserControl"/> class.
        /// </summary>
        public SearchBoxUserControl()
        {
            InitializeComponent();

#if DEBUG
            SearchKeyword.Text = "egr";
#endif
        }

        private void SearchAction_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (SearchListener != null)
            {
                SearchListener.OnSearch(SearchKeyword.Text);
            }
        }

        private void SearchKeyword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (SearchListener != null)
                {
                    SearchListener.OnSearch(SearchKeyword.Text);
                }
            }
        }
    }
}
