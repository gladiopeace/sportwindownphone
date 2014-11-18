using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DiPaSport;

namespace Com.IT.DiPaSport.View.Controls
{
    public partial class TextBoxEx : UserControl
    {
        /// <summary>
        /// The placeholder text property
        /// </summary>
        public static DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(TextBoxEx), null);

        /// <summary>
        /// The text property
        /// </summary>
        public static DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxEx), null);

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>
        /// The placeholder text.
        /// </value>
        public string PlaceholderText
        {
            get
            {
                return (string)GetValue(PlaceholderTextProperty);
            }
            set
            {
                SetValue(PlaceholderTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get
            {
                string text = (string)GetValue(TextProperty);
                return text == String.Empty ? ValueEx.Text : text;
            }

            set
            {
                SetValue(TextProperty, value);
                ValueEx.Text = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxEx"/> class.
        /// </summary>
        public TextBoxEx()
        {
            InitializeComponent();
            ValueEx.TextChanged += ValueEx_TextChanged;
        }

        /// <summary>
        /// Handles the TextChanged event of the ValueEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        void ValueEx_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = ValueEx.Text;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is high light.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is high light; otherwise, <c>false</c>.
        /// </value>
        public bool IsHighLight
        {
            get
            {
                string s = ThizBorder.Style.ToString();
                return false;
            }
            set
            {
                if (value)
                {
                    ThizBorder.Style = (Style)App.Current.Resources["BorderHighLightStyle"];
                }
                else
                {
                    ThizBorder.Style = (Style)App.Current.Resources["BorderStyle"];
                }
            }
        }


    }
}
