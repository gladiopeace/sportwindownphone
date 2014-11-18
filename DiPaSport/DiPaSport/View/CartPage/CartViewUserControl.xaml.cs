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
using Com.IT.DiPaSport.View.BasePage;
using System.Diagnostics;
using Com.IT.DiPaSport.Database;
using Coding4Fun.Toolkit.Controls;
using Com.IT.DiPaSport.Resources;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Com.IT.DiPaSport.View
{
    public partial class CartViewUserControl : BaseUserControl
    {

        /// <summary>
        /// 
        /// </summary>
        public interface OnCartUpdateQuantity
        {
            void OnUpdateQuantity(string entityID, int quantity);
        }

        /// <summary>
        /// The thiz instance
        /// </summary>
        private static CartViewUserControl ThizInstance;

        /// <summary>
        /// The on check coupon code listener
        /// </summary>
        public JSONCallbackListener OnCheckCouponCodeListener { private get; set; }

        /// <summary>
        /// Gets or sets the on order listener.
        /// </summary>
        /// <value>
        /// The on order listener.
        /// </value>
        public JSONCallbackListener OnOrderListener { private get; set; }

        /// <summary>
        /// The on cart changed listener
        /// </summary>
        public OnCartUpdateQuantity OnCartUpdateQuantityListener;


        /// <summary>
        /// The cart order
        /// </summary>
        private CartOrderViewModel CartOrder;

        /// <summary>
        /// The discount confirm
        /// </summary>
        InputPrompt discountConfirm;


        /// <summary>
        /// The discount amount
        /// </summary>
        float DiscountAmount = .0f;
        string CouponCode = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="CartViewUserControl"/> class.
        /// </summary>
        public CartViewUserControl()
        {
            InitializeComponent();
            Width = double.NaN;
            Height = Constants.Dimens.SCREEN_HEIGHT;
            Name = Constants.ScreensName.CART;

            ThizInstance = this;
            CartOrder = new CartOrderViewModel();
        }
        /// <summary>
        /// Called when [load].
        /// </summary>
        public override void OnLoad()
        {
            IList<ProductTable> orderlist = CartOrder.GetOrder();
            OrderList.Items.Clear();

            float total = .0f;
            float tax = .0f;
            float totalWithTax = .0f;

            foreach (var p in orderlist)
            {
                OrderList.Items.Add(p);
                total += (p.SpecialPrice > 0 ? p.SpecialPrice : p.Price) * p.Quantity;
            }

            if (DiscountAmount > 0)
            {
                total -= DiscountAmount;
                DiscountValue.Text = string.Format("€{0:0.00}", DiscountAmount);
                Discount.Visibility = Visibility.Visible;
                DiscountValue.Visibility = Visibility.Visible;
            }
            else
            {
                DiscountValue.Text = string.Empty;
                Discount.Visibility = Visibility.Collapsed;
                DiscountValue.Visibility = Visibility.Collapsed;
            }

            tax = (total / 100) * 22; // tax = 22%
            totalWithTax = total + tax;
            TotalWithoutTax.Text = String.Format("€{0:0.00}", total); ;
            TaxVAT.Text = String.Format("€{0:0.00}", tax);
            TotalWithTax.Text = String.Format("€{0:0.00}", totalWithTax); ;
        }

        /// <summary>
        /// Handles the Tap event of the CartQuantity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void CartQuantity_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string entityId = (string)(sender as TextBlock).Tag;
            if (OnCartUpdateQuantityListener != null)
            {
                int currentQuantity = -1;
                foreach (ProductTable p in OrderList.Items)
                {
                    if (p.ID == entityId)
                    {
                        currentQuantity = p.Quantity;
                    }
                }
                OnCartUpdateQuantityListener.OnUpdateQuantity(entityId, currentQuantity);
            }

        }

        /// <summary>
        /// Handles the Click event of the CartDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CartDelete_Click(object sender, RoutedEventArgs e)
        {
            string entityId = (string)(sender as Button).Tag;
            Debug.WriteLine("id: " + entityId);

            MessagePrompt confirmDel = new MessagePrompt()
            {
                Message = AppResources.ConfirmRemoveCart,
                IsCancelVisible = true,
                Tag = entityId
            };
            confirmDel.Completed += cartDeleteConfirm_OnCompleted;
            confirmDel.Show();
        }

        /// <summary>
        /// Handles the OnCompleted event of the cartDeleteConfirm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="result">The <see cref="PopUpEventArgs{System.String, PopUpResult}"/> instance containing the event data.</param>
        void cartDeleteConfirm_OnCompleted(object sender, PopUpEventArgs<string, PopUpResult> result)
        {
            if (result.PopUpResult == PopUpResult.Ok)
            {
                string entityId = (string)(sender as MessagePrompt).Tag;
                OrderProducts order = new OrderProducts();
                order.DeleteProduct(entityId);
                OnLoad();
            }
        }

        /// <summary>
        /// Handles the Click event of the Discount control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Discount_Click(object sender, RoutedEventArgs e)
        {
            if (!LoginModel.IsLogin)
            {
                new MessagePrompt()
                {
                    Message = AppResources.CartConfirmToPay
                }.Show();
                return;
            }

            if (OrderList.Items.Count == 0)
            {
                new MessagePrompt()
                {
                    Message = AppResources.CartEmpty
                }.Show();
                return;
            }


            discountConfirm = new InputPrompt()
            {
                Title = AppResources.CartCouponContent,
                IsCancelVisible = true,
            };
#if DEBUG
            discountConfirm.Value = "BENV014DIPA";
#endif
            discountConfirm.Completed += discount_Completed;
            discountConfirm.Show();
        }

        /// <summary>
        /// Handles the Completed event of the discount control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PopUpEventArgs{System.String, PopUpResult}"/> instance containing the event data.</param>
        void discount_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.Result == string.Empty)
            {
                new ToastPrompt()
                {
                    Message = AppResources.CartDiscountCodeEmpty
                }.Show();
                return;
            }

            if (e.PopUpResult == PopUpResult.Ok)
            {
                CouponCode = e.Result;
                DiscountModel discountModel = new DiscountModel() { RegisterCallback = new CheckCouponCode() };
                discountModel.CheckCouponCode(CouponCode);
            }
        }

        /// <summary>
        /// Handles the Click event of the Order control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (!LoginModel.IsLogin)
            {
                new MessagePrompt()
                {
                    Message = AppResources.CartConfirmToPay
                }.Show();
                return;
            }

            if (OrderList.Items.Count == 0)
            {
                new MessagePrompt()
                {
                    Message = AppResources.CartEmpty
                }.Show();
                return;
            }

            MessagePrompt orderConfirm = new MessagePrompt()
            {
                Message = string.Format("{0}: {1}", AppResources.CartOrderConfirmContent, TotalWithTax.Text),
                IsCancelVisible = true
            };
            orderConfirm.Completed += orderConfirm_Completed;
            orderConfirm.Show();
        }

        void orderConfirm_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                CheckoutModel checkount = new CheckoutModel()
                {
                    RegisterCallback = new Order()
                };
                
                if (CouponCode.Length > 0)
                {
                    checkount.Checkout(CartOrder.GetOrder(), CouponCode);
                }
                else
                {
                    checkount.Checkout(CartOrder.GetOrder());
                }

                new ToastPrompt()
                {
                    Message = AppResources.CartOrderSuccess
                }.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class CheckCouponCode : JSONCallbackListener
        {
            public override void OnResults(JObject results)
            {
                if ((int)results[Constants.JSON_TAG.ERROR_CODE] == 0)
                {
                    JObject data = (JObject)results["data"];
                    bool isValid = DiscountModel.ValidateCode(data);
                    bool isUsed = DiscountModel.IsUsed(results);

                    if (!isValid)
                    {
                        new ToastPrompt()
                        {
                            Message = AppResources.CartDiscountExpired
                        }.Show();
                        ThizInstance.CouponCode = string.Empty;
                        ThizInstance.DiscountAmount = .0f;
                        return;
                    }

                    if (isUsed)
                    {
                        new ToastPrompt()
                        {
                            Message = AppResources.CartCouponIsUsed
                        }.Show();
                        ThizInstance.CouponCode = string.Empty;
                        ThizInstance.DiscountAmount = .0f;
                        return;
                    }

                    // get amount
                    ThizInstance.DiscountAmount = float.Parse((string)data["discount_amount"], CultureInfo.InvariantCulture.NumberFormat);
                }
                else
                {
                    new ToastPrompt()
                    {
                        Message = AppResources.CartCouponInvail
                    }.Show();
                    ThizInstance.DiscountAmount = .0f;
                }
                ThizInstance.OnLoad();
            }

            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                
            }

            public override void StartRequest()
            {
                if (ThizInstance.OnCheckCouponCodeListener != null)
                {
                    ThizInstance.OnCheckCouponCodeListener.StartRequest();
                }
            }

            public override void EndRequest()
            {
                if (ThizInstance.OnCheckCouponCodeListener != null)
                {
                    ThizInstance.OnCheckCouponCodeListener.EndRequest();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private class Order : JSONCallbackListener
        {
            public override void OnResults(JObject results)
            {
                if ((int)results[Constants.JSON_TAG.ERROR_CODE] == 0)
                {
                    new ToastPrompt()
                    {
                        Message = AppResources.CartOrderSuccess
                    }.Show();
                    ThizInstance.CartOrder.ClearOrder();
                    ThizInstance.DiscountAmount = .0f;
                    ThizInstance.OnLoad();

                    string orderID = (string)results["IdOrde"];

                    new MessagePrompt()
                    {
                        Title = string.Format("{0}: {1}", AppResources.CartOrderNumber, orderID),
                        Message = "Your order has been successfully completed. You will receive an email with all the information relating to your order."
                    }.Show();
                }
                else
                {
                    new ToastPrompt()
                    {
                        Message = AppResources.CartOrderFail
                    }.Show();
                }
            }

            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                new ToastPrompt()
                {
                    Message = AppResources.ServerNotResponse
                }.Show();
            }

            public override void StartRequest()
            {
                if (ThizInstance.OnOrderListener != null)
                {
                    ThizInstance.OnOrderListener.StartRequest();
                }
            }

            public override void EndRequest()
            {
                if (ThizInstance.OnOrderListener != null)
                {
                    ThizInstance.OnOrderListener.EndRequest();
                }
            }
        }
    }
}
