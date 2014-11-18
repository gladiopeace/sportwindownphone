using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Com.IT.DiPaSport.Resources;
using Com.IT.DiPaSport.Model.Interfaces;
using Microsoft.Phone.Tasks;
using Com.IT.DiPaSport.View;
using Com.IT.DiPaSport.Model;
using Newtonsoft.Json.Linq;
using System.IO.IsolatedStorage;
using Com.IT.DiPaSport.View.AccountPage;
using Coding4Fun.Toolkit.Controls;
using Com.IT.DiPaSport.Model.UserControlManager;
using Comfirm.AlphaMail.Services.Client;
using Com.IT.DiPaSport.View.ContactPage;
using Com.IT.DiPaSport.View.CartPage;
using Com.IT.DiPaSport.Database;
using Com.IT.DiPaSport.View.HomePage;
using Com.IT.DiPaSport.View.SearchPage;
using System.Threading;

namespace Com.IT.DiPaSport
{
    public partial class MainPage : BasePage, HomeActionListener, OnRequestListener,
        BottomBarUserControl.BottomBarListener, AccountViewUserControl.AccountViewListener,
        SeachUserControl.OnSearchAction, CartViewUserControl.OnCartUpdateQuantity, 
        UserControlManager.UserControlChangedListener
    {
        public static HomeViewUserControl MainHomeView;
        public static SearchViewUserControl MainSearchView;
        public static AccountViewUserControl MainAccountView;
        public static CartViewUserControl MainCartView;
        public static ContactViewUserControl MainContactView;

        public static MainPage ThizInstance;

        /// <summary>
        /// The order
        /// </summary>
        private OrderProducts Order;
        /// <summary>
        /// The pages
        /// </summary>
        private readonly List<UserControl> Pages = new List<UserControl>();

        /// <summary>
        /// The user control manager
        /// </summary>
        private readonly UserControlManager UCManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("language"))
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo((string)settings["language"]);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo((string)settings["language"]);
            }

            InitializeComponent();
            UCManager = new UserControlManager(ContentPanel.Children)
            {
                OnUserControlListener = this
            };

            // Init Pages
            InitPages();
            ThizInstance = this;

            Order = new OrderProducts();

            Loaded += MainPage_Loaded;
            
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            IEnumerable<System.Windows.Navigation.JournalEntry> b = NavigationService.BackStack;

            var previousPage = b.FirstOrDefault();

            if (previousPage != null && previousPage.Source.ToString().StartsWith("/View/LanguagePage.xaml"))
            {
                NavigationService.RemoveBackEntry();
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            /*
             * Pin app to start screen
            try
            {
                StandardTileData tile = new StandardTileData
                {
                    Title = AppResources.AppName,
                    BackgroundImage = new Uri("Background.png", UriKind.RelativeOrAbsolute)
                };

                ShellTile.Create(new Uri("/View/MainPage.xaml", UriKind.RelativeOrAbsolute), tile);
            }
            catch (InvalidOperationException ex)
            {
            }
             */
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (UCManager.Count() > 1)
            {
                UCManager.PopUserControl();
            }
            else
            {
                base.OnBackKeyPress(e);
            }
        }
        #region Request products

        public void OnRequestProduct()
        {
            CameraCaptureTask requestProduct = new CameraCaptureTask();
            requestProduct.Completed += requestProduct_Completed;
            requestProduct.Show();

        }

        void requestProduct_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {   
                ProductRequestViewUserControl productRequest = new ProductRequestViewUserControl()
                {
                    HomeAction = this,
                    ImageFile = e.ChosenPhoto
                };
                UCManager.PushUserControl(productRequest);

                /*
                MailMessage mail = new MailMessage()
                {
                    AccountType = MailMessage.AccountTypeEnum.Gmail,
                    UserName = "customer.dipasport@gmail.com",
                    Password = "aM8^M*Sc",
                    From = "customer.dipasport@gmail.com",
                    Body = "<i>test</i><br><b>welcome</b>",
                    To = "vngocvan@gmail.com",
                    Subject = "Test email",
                };
                //mail.AddAttachment(e.OriginalFileName);
                mail.Error += mail_Error;
                mail.MailSent += mail_MailSent;
                mail.Progress += mail_Progress;
                mail.SetCustomSMTPServer("smtp.gmail.com", 465, true);
                mail.Send();
                */
            }
        }

        #endregion


        #region Home

        /// <summary>
        /// Called when [search by qr code].
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnSearchByQRCode()
        {
        }

        /// <summary>
        /// Called when [search].
        /// </summary>
        /// <param name="keyword"></param>
        public void OnSearch(string keyword)
        {
            //MessageBox.Show(keyword);
            UCManager.ReplaceUserControl(MainSearchView);
            MainSearchView.OnSearch(keyword);
        }

        /// <summary>
        /// Called when tap to a product in the homepage
        /// </summary>
        public void OnProductView()
        {

        }

        /// <summary>
        /// Called when [open category].
        /// </summary>
        public void OnOpenCategory()
        {
            SearchByCategoryUserControl category = new SearchByCategoryUserControl()
            {
                HomeListener = this,
                FetchJSONCallbackListener = new OnSearchListener(),
                SearchActionListener = this
            };
            UCManager.PushUserControl(category);
        }

        /// <summary>
        /// Called when [close category].
        /// </summary>
        public void OnCloseCategory()
        {
            UCManager.PopUserControl();
        }

        /// <summary>
        /// Called when [open quantity].
        /// </summary>
        /// <param name="p">The p.</param>
        public void OnOpenQuantity(ProductTable p)
        {
            if (ThizInstance.Order.IsExist(p.ID))
            {
                new ToastPrompt()
                {
                    Message = AppResources.CartProductExisting
                }.Show();
                return;
            }

            CartQuantityViewUserControl quantity = new CartQuantityViewUserControl()
            {
                QuantityChangedListener = new AddToCart(),
                CurrentProduct = p
            };
            UCManager.PushUserControl(quantity);
        }

        public void OnUpdateQuantity(string entityID, int quantity)
        {
            CartQuantityViewUserControl quantityUpdate = new CartQuantityViewUserControl()
            {
                QuantityChangedListener = new AddToCart(),
                EntityID = entityID,
                QuantityUpdated = quantity
            };
            UCManager.PushUserControl(quantityUpdate);
        }

        public void OnCloseQuantity()
        {
            
        }

        public void OnOpenDetail(ProductTable p)
        {
            ProductDetailsViewUserControl productDetail = new ProductDetailsViewUserControl(p)
            {
                HomeListener =  this, // need callback to function StartRequest(), EndRequest()
                SearchActionListener = this // need callback to function OnCloseDetail()
            };
            UCManager.PushUserControl(productDetail);
        }

        public void OnOpenSlideImage(List<string> images, string name)
        {
            SlideImageViewUserControl slideImage = new SlideImageViewUserControl()
            {
                HomeAction = this,
                Images = images,
                ProductName = name
            };
            UCManager.PushUserControl(slideImage);
        }

        public void OnCloseDetail()
        {
            UCManager.PopUserControl();
        }

        public void OnLoginRequired()
        {
            UCManager.ReplaceUserControl(MainAccountView);
        }

        public void OnBeginRequestCategory()
        {
            StartRequest(AppResources.StatusLoading);
        }

        public void OnEndRequestCategory()
        {
            EndRequest();
        }
        #endregion


        #region Bottom bar events

        public void OnHomeSelected()
        {
            UCManager.ReplaceUserControl(MainHomeView);
        }

        public void OnSearchSelected()
        {
            UCManager.ReplaceUserControl(MainSearchView);
        }

        public void OnAccountSelected()
        {
            UserControl tmp = (ContentPanel.Children.First() as UserControl);
            if (LoginModel.IsLogin)
            {
                LogoutViewUserControl logout = new LogoutViewUserControl() { AccountListener = this, };
                UCManager.ReplaceUserControl(logout);
            }
            else
            {
                UCManager.ReplaceUserControl(MainAccountView);
            }
        }

        public void OnCartSelected()
        {
            UCManager.ReplaceUserControl(MainCartView);
        }

        public void OnContactSelected()
        {
            UCManager.ReplaceUserControl(MainContactView);
        }

        /// <summary>
        /// Automatics the hide tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        private void AutoHideTab(UserControl tab)
        {
            tab.Visibility = System.Windows.Visibility.Visible;
            foreach (var page in Pages)
            {
                if (page.Name == tab.Name) continue;
                page.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the pages.
        /// </summary>
        private void InitPages()
        {
            // Register bottom bar listener
            BottomBarMenu.OnBottomBarListener = this;

            
            if (MainHomeView == null)
            {
                MainHomeView = new HomeViewUserControl()
                {
                    HomeListener = this,
                    DetailActionListener = this,
                    Tag = 0
                };
            }

            if (MainSearchView == null)
            {
                MainSearchView = new SearchViewUserControl()
                {
                    SearchCallbackListener = new OnSearchListener(),
                    SearchActionListener = this,
                    Tag = 1
                };
            }

            if (MainAccountView == null)
            {
                MainAccountView = new AccountViewUserControl()
                {
                    AccountListener = this,
                    LoginCallback = new LoginCallbackListener(),
                    LostPasswordCallback = new LostPasswordCallbackListener(),
                    Tag = 2
                };
            }

            if (MainCartView == null)
            {
                MainCartView = new CartViewUserControl()
                {
                    OnCartUpdateQuantityListener = this,
                    OnCheckCouponCodeListener = new Checkout(),
                    OnOrderListener = new Checkout(),
                    Tag = 3
                };
            }

            if (MainContactView == null)
            {
                MainContactView = new ContactViewUserControl()
                {
                    ContactListener = new ContactListener(),
                    Tag = 4
                };
            }

            // Assign all tab to list
            //Pages.AddRange(new UserControl[] { MainHomeView, MainSearchView, MainAccountView, MainCartView, MainContactView });

            // Init main page as default
            UCManager.ReplaceUserControl(MainHomeView);
        }

        #region Account Pages
        public void OnLogin()
        {
        }

        public void OnRegister()
        {
            RegisterViewUserControl register = new RegisterViewUserControl()
            {
                AccountListener = this,
                RegisterCallbackListener = new OnRegisterListener()
            };
            UCManager.PushUserControl(register);
        }

        public void OnLostPassword()
        {

        }


        public void OnRegisterBack()
        {
            UCManager.PopUserControl();
        }


        public void OnLogout()
        {
            OnAccountSelected();
        }


        /// <summary>
        /// Called when [open customer type].
        /// </summary>
        /// <param name="selectedIndex"></param>
        public void OnOpenCustomerType(int selectedIndex)
        {
            CustomerTypeViewUserControl customerType = new CustomerTypeViewUserControl()
            {
                AccountListener = this,
                Tag = selectedIndex
            };
            UCManager.PushUserControl(customerType);
        }

        /// <summary>
        /// Called when [selected customer type].
        /// </summary>
        /// <param name="cusType">Type of the cus.</param>
        /// <param name="index">The index.</param>
        public void OnSelectedCustomerType(string cusType, int index)
        {
            UCManager.PopUserControl();
            (UCManager.getCurrent() as RegisterViewUserControl).UpdateCustomerType(cusType, index);
        }

        /// <summary>
        /// Called when [open country screen].
        /// </summary>
        /// <param name="indexSelected">The index selected.</param>
        public void OnOpenCountres(int selectedIndex)
        {
            CountriesViewUserControl country = new CountriesViewUserControl()
            {
                AccountListener = this,
                Tag = selectedIndex
            };
            UCManager.PushUserControl(country);
        }

        /// <summary>
        /// Called when [selected country].
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="code">The code.</param>
        public void OnSelectedCountry(CountryObject country, int selectedIndex)
        {
            UCManager.PopUserControl();
            (UCManager.getCurrent() as RegisterViewUserControl).UpdateCountry(country.Name, selectedIndex);
        }
        #endregion

        #region Login event callback

        /// <summary>
        /// Listener will be called when user loginCallbackListener
        /// </summary>
        private class LoginCallbackListener : JSONCallbackListener
        {
            public override void OnResults(JObject results)
            {
                if ((int)results[Constants.JSON_TAG.ERROR_CODE] == 0)
                {
                    JObject data = (JObject)results[Constants.JSON_TAG.DATA];
                    if (data != null)
                    {
                        LoginModel.SaveLoginSettings(data);

                        // Switch to homepage
                        ThizInstance.OnHomeSelected();

                        // show toast
                        ToastPrompt loginSuccess = new ToastPrompt()
                        {
                            Message = AppResources.LoginSuccess
                        };
                        loginSuccess.Show();
                    }
                }
                else
                {
                    new MessagePrompt()
                    {
                        Title = AppResources.LoginTitle,
                        Message = AppResources.LoginFail
                    }.Show();
                }
            }

            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                MessageBox.Show(AppResources.NoNetwork);
            }

            public override void StartRequest()
            {
                ThizInstance.StartRequest(AppResources.Logining);
            }

            public override void EndRequest()
            {
                ThizInstance.EndRequest();
            }
        }

        #endregion

        #region Lost password event callback
        private class LostPasswordCallbackListener : JSONCallbackListener
        {
            public override void OnResults(JObject results)
            {
                if ((int)results[Constants.JSON_TAG.ERROR_CODE] == 0)
                {
                    string message = (string)results[Constants.JSON_TAG.MESSAGE];
                    new MessagePrompt()
                    {
                        Title = AppResources.LostPasswordTitle,
                        Message = message
                    }.Show();
                }
                else
                {
                    new MessagePrompt()
                    {
                        Title = AppResources.LostPasswordTitle,
                        Message = AppResources.LostPasswordMsgFail
                    }.Show();
                }
            }

            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                MessageBox.Show(AppResources.NoNetwork);
            }

            public override void StartRequest()
            {
                ThizInstance.StartRequest();
            }

            public override void EndRequest()
            {
                ThizInstance.EndRequest();
            }
        }
        #endregion

        #region Category event callback
       
        #endregion

        #region Registration
        private class OnRegisterListener : JSONCallbackListener
        {
            public override void OnResults(Newtonsoft.Json.Linq.JObject results)
            {
                if ((int)results[Constants.JSON_TAG.ERROR_CODE] == 0)
                {
                    string msg = (string)results[Constants.JSON_TAG.MESSAGE];
                    new MessagePrompt()
                    {
                        Message = msg
                    }.Show();
                    ThizInstance.UCManager.PopUserControl();
                }
                else
                {
                    new ToastPrompt()
                    {
                        Message = AppResources.NoNetwork
                    }.Show();
                }
            }

            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                new MessagePrompt()
                {
                    Message = AppResources.NoNetwork
                }.Show();
            }

            public override void StartRequest()
            {
                ThizInstance.StartRequest();
            }

            public override void EndRequest()
            {
                ThizInstance.EndRequest();
            }
        }
        #endregion

        #region Contact

        private class ContactListener : ContactViewUserControl.ContactViewListener
        {
            private SocialViewUserControl socialView;

            public void OnOpenFacebook()
            {
                socialView = new SocialViewUserControl()
                {
                    URL = Constants.SOCIAL.FACEBOOK,
                    ContactListener = this
                };
                ThizInstance.UCManager.PushUserControl(socialView);
            }

            public void OnOpenTwitter()
            {
                socialView = new SocialViewUserControl()
                {
                    URL = Constants.SOCIAL.TWITTER,
                    ContactListener = this
                };
                ThizInstance.UCManager.PushUserControl(socialView);
            }

            public void OnOpenYoutube()
            {
                socialView = new SocialViewUserControl()
                {
                    URL = Constants.SOCIAL.YOUTUBE,
                    ContactListener = this
                };
                ThizInstance.UCManager.PushUserControl(socialView);
            }

            public void OnOpenMap()
            {
                socialView = new SocialViewUserControl()
                {
                    URL = Constants.SOCIAL.MAP,
                    ContactListener = this
                };
                ThizInstance.UCManager.PushUserControl(socialView);
            }

            public void OnBack()
            {
                ThizInstance.OnBack();
            }

            public void StartRequest()
            {
                ThizInstance.StartRequest();
            }

            public void EndRequest()
            {
                ThizInstance.EndRequest();
            }
        }

        #endregion

        #region Search
        private class OnSearchListener : JSONCallbackListener
        {
            /// <summary>
            /// Called when download and convert to JObject successful.
            /// </summary>
            /// <param name="results">The results.</param>
            public override void OnResults(JObject results)
            {
                if ((int)results[Constants.JSON_TAG.ERROR_CODE] == 1)
                {
                    SearchNotFoundViewUserControl searchNotFound = new SearchNotFoundViewUserControl()
                    {
                        HomeAction = ThizInstance
                    };
                    ThizInstance.UCManager.PushUserControl(searchNotFound);
                }
            }

            /// <summary>
            /// Call when errors related connection or convert to JObject fail.
            /// </summary>
            /// <param name="ErrorCode">The error code.</param>
            /// <param name="ErrorMessage">The error message.</param>
            public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
            {
                new ToastPrompt()
                {
                    Message = AppResources.ServerNotResponse
                }.Show();
                
            }

            /// <summary>
            /// Begins the loading.
            /// </summary>
            public override void StartRequest()
            {
                ThizInstance.StartRequest(AppResources.StatusSearching);
            }

            /// <summary>
            /// Ends the loading.
            /// </summary>
            public override void EndRequest()
            {
                ThizInstance.EndRequest();
            }
        }
        #endregion

        #region Add to cart
        private class AddToCart : CartQuantityViewUserControl.OnQuantityChangedListener
        {

            public void OnQuantityChanged(ProductTable p)
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(Constants.JSON_TAG.LOGIN.ToString()))
                {
                    LoginObject login = (LoginObject)settings[Constants.JSON_TAG.LOGIN.ToString()];
                    p.UserID = login.UserID;
                }
                bool isSuccess = ThizInstance.Order.AddProduct(p);
                if (isSuccess)
                {
                    new ToastPrompt()
                    {
                        Message = AppResources.CartAddSuccess
                    }.Show();
                }
                else
                {
                }
            }

            public void OnQuantityCancel()
            {
                ThizInstance.UCManager.PopUserControl();
            }

            public void OnQuantityUpdated(string entityId, int quantity)
            {
                ThizInstance.Order.UpdateQuantity(entityId, quantity);
                MainCartView.OnLoad();
            }
        }


        #endregion

        #region Order
        private class Checkout : JSONCallbackListener
        {
            public override void StartRequest()
            {
                ThizInstance.StartRequest(AppResources.PleaseWait);
            }

            public override void EndRequest()
            {
                ThizInstance.EndRequest();
            }
        }
        #endregion

        /// <summary>
        /// Begins the loading.
        /// </summary>
        public void StartRequest()
        {
            WaitIndicator.Text = AppResources.StatusLoading;
            WaitIndicator.IsVisible = true;
        }

        /// <summary>
        /// Starts the request.
        /// </summary>
        /// <param name="message">The message.</param>
        public void StartRequest(string message)
        {
            WaitIndicator.Text = message;
            WaitIndicator.IsVisible = true;
        }

        /// <summary>
        /// Ends the loading.
        /// </summary>
        public void EndRequest()
        {
            WaitIndicator.IsVisible = false;
        }

        /// <summary>
        /// Called when [back].
        /// </summary>
        public void OnBack()
        {
            UCManager.PopUserControl();
        }


        #region Listener when user control changed
        public void OnUserControlAdded(View.BasePage.BaseUserControl usercontrol)
        {
            if (usercontrol.Tag != null)
            {
                int index = int.Parse(usercontrol.Tag.ToString());
                BottomBarMenu.SelectedAt(index);
            }
        }

        public void OnuserControlRemoved(View.BasePage.BaseUserControl usercontrol)
        {
            
        }
        #endregion
    }
}