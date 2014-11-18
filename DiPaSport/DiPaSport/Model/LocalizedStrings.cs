using Com.IT.DiPaSport.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.it.DiPaSport.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizedStrings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedStrings"/> class.
        /// </summary>
        public LocalizedStrings()
        {

        }

        /// <summary>
        /// The localized strings
        /// </summary>
        private readonly static AppResources localizedStrings = new AppResources();

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public static AppResources Lang { get { return localizedStrings; } }
    }
}
