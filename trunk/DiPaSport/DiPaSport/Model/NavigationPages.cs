using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class NavigationPages
    {
        /// <summary>
        /// Gets the main page.
        /// </summary>
        /// <value>
        /// The main page.
        /// </value>
        public static Uri MainPage { get { return new Uri("/View/MainPage.xaml", UriKind.Relative); } }
    }
}
