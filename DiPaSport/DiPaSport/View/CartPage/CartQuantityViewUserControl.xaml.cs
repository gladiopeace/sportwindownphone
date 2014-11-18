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
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View.CartPage
{
    public partial class CartQuantityViewUserControl : BaseUserControl
    {

        /// <summary>
        /// 
        /// </summary>
        public interface OnQuantityChangedListener
        {
            void OnQuantityChanged(ProductTable p);
            void OnQuantityUpdated(string entityId, int quantity);
            void OnQuantityCancel();
        }

        /// <summary>
        /// Gets or sets the quantity updated.
        /// </summary>
        /// <value>
        /// The quantity updated.
        /// </value>
        [System.ComponentModel.DefaultValue(-1)]
        public int QuantityUpdated { private get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        /// The entity identifier.
        /// </value>
        [System.ComponentModel.DefaultValue(null)]
        public string EntityID { private get; set; }

        /// <summary>
        /// Gets or sets the current product.
        /// </summary>
        /// <value>
        /// The current product.
        /// </value>
        public ProductTable CurrentProduct { private get; set; }
        /// <summary>
        /// The quantity changed listener
        /// </summary>
        public OnQuantityChangedListener QuantityChangedListener;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartQuantityViewUserControl"/> class.
        /// </summary>
        public CartQuantityViewUserControl()
        {
            InitializeComponent();
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;
            Name = Constants.ScreensName.CART_QUANTITY;

            Loaded += CartQuantityViewUserControl_Loaded;
        }

        void CartQuantityViewUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (QuantityUpdated != 0)
            {
                Quantity.Text = QuantityUpdated.ToString();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (QuantityChangedListener != null)
            {
                QuantityChangedListener.OnQuantityCancel();
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (QuantityChangedListener != null)
            {
                int quantity = int.Parse(Quantity.Text);
                if (QuantityUpdated != 0)
                {
                    QuantityChangedListener.OnQuantityUpdated(EntityID, quantity);
                }
                else
                {                    
                    CurrentProduct.Quantity = quantity;
                    QuantityChangedListener.OnQuantityChanged(CurrentProduct);
                }
                QuantityChangedListener.OnQuantityCancel();
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            int quantity = int.Parse(Quantity.Text);
            --quantity;
            if (quantity <= 1)
            {
                quantity = 1;
                Prev.IsEnabled = false;
            }
            else
            {
                Next.IsEnabled = true;
            }
            Quantity.Text = quantity.ToString();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int quantity = int.Parse(Quantity.Text);
            ++quantity;
            if (quantity >= 50)
            {
                Next.IsEnabled = false;
                quantity = 50;
            }
            else
            {
                Prev.IsEnabled = true;
            }
            Quantity.Text = quantity.ToString();
        }

        private void QuantityBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (QuantityChangedListener != null)
            {
                QuantityChangedListener.OnQuantityCancel();
            }
        }
    }
}
