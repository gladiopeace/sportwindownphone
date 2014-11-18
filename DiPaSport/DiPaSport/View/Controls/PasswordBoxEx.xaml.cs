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
    public partial class PasswordBoxEx : UserControl
    {
        /// <summary>
        /// The placeholder text property
        /// </summary>
        public static DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PasswordBoxEx), null);


        /// <summary>
        /// The password property
        /// </summary>
        public static DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxEx), null);

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
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get
            {
                string password = (string)GetValue(PasswordProperty);
                return password == String.Empty ? ValueEx.Password : password;
            }
            set
            {
                SetValue(PasswordProperty, value);
                ValueEx.Password = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordBoxEx"/> class.
        /// </summary>
        public PasswordBoxEx()
        {
            InitializeComponent();
            ValueEx.PasswordChanged += ValueEx_PasswordChanged;
        }

        void ValueEx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = ValueEx.Password;
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
