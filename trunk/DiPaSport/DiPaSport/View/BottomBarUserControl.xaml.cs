using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using Com.IT.DiPaSport.View.BasePage;

namespace Com.IT.DiPaSport.View
{
    public partial class BottomBarUserControl : UserControl
    {
        /// <summary>
        /// Broadcast listener when an item at bottom bar selected
        /// </summary>
        public interface BottomBarListener
        {
            void OnHomeSelected();
            void OnSearchSelected();
            void OnAccountSelected();
            void OnCartSelected();
            void OnContactSelected();
        }

        /// <summary>
        /// Gets or sets the on bottom bar listener.
        /// </summary>
        /// <value>
        /// The on bottom bar listener.
        /// </value>
        public BottomBarListener OnBottomBarListener { private get; set; }

        /// <summary>
        /// Gets the background selected.
        /// </summary>
        /// <value>
        /// The background selected.
        /// </value>
        private static SolidColorBrush BackgroundSelected
        {
            get
            {
                return new SolidColorBrush()
                {
                    Color = Color.FromArgb(255, 220, 220, 220)
                };
            }
        }

        /// <summary>
        /// Gets the background normal.
        /// </summary>
        /// <value>
        /// The background normal.
        /// </value>
        private static SolidColorBrush BackgroundNormal
        {
            get
            {
                return new SolidColorBrush()
                {
                    Color = Color.FromArgb(0, 255, 255, 255)
                };
            }
        }

        /// <summary>
        /// The list button bottom bar
        /// </summary>
        private List<Button> ListButtonBottomBar = new List<Button>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BottomBarUserControl"/> class.
        /// </summary>
        public BottomBarUserControl()
        {
            InitializeComponent();
            ListButtonBottomBar.AddRange(new Button[] { MenuHome, MenuSearch, MenuAccount, MenuCart, MenuContact });
        }
        //{#FFDCDCDC}
        /// <summary>
        /// Handles the Tap event of the MenuHome control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void MenuHome_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (sender as Button).Background = BackgroundSelected;
            clearAllBackground(sender as Button);

            if (OnBottomBarListener != null)
            {
                OnBottomBarListener.OnHomeSelected();
            }
        }

        /// <summary>
        /// Handles the Tap event of the MenuSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void MenuSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (sender as Button).Background = BackgroundSelected;
            clearAllBackground(sender as Button);

            if (OnBottomBarListener != null)
            {
                OnBottomBarListener.OnSearchSelected();
            }
        }

        /// <summary>
        /// Handles the Tap event of the MenuAccount control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void MenuAccount_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (sender as Button).Background = BackgroundSelected;
            clearAllBackground(sender as Button);

            if (OnBottomBarListener != null)
            {
                OnBottomBarListener.OnAccountSelected();
            }
        }

        /// <summary>
        /// Handles the Tap event of the MenuCart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void MenuCart_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (sender as Button).Background = BackgroundSelected;
            clearAllBackground(sender as Button);

            if (OnBottomBarListener != null)
            {
                OnBottomBarListener.OnCartSelected();
            }
        }

        /// <summary>
        /// Handles the Tap event of the MenuContact control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void MenuContact_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (sender as Button).Background = BackgroundSelected;
            clearAllBackground(sender as Button);

            if (OnBottomBarListener != null)
            {
                OnBottomBarListener.OnContactSelected();
            }
        }

        /// <summary>
        /// Clears all background.
        /// </summary>
        /// <param name="current">The current.</param>
        private void clearAllBackground(Button current)
        {
            foreach (var btn in ListButtonBottomBar)
            {
                if (btn.Name == current.Name) continue;
                btn.Background = BackgroundNormal;
            }
        }

        /// <summary>
        /// Selecteds at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SelectedAt(int index)
        {
            switch (index)
            {
                case 0:
                    MenuHome.Background = BackgroundSelected;
                    clearAllBackground(MenuHome);
                    break;
                case 1:
                    MenuSearch.Background = BackgroundSelected;
                    clearAllBackground(MenuSearch);
                    break;
                case 2:
                    MenuAccount.Background = BackgroundSelected;
                    clearAllBackground(MenuAccount);
                    break;
                case 3:
                    MenuCart.Background = BackgroundSelected;
                    clearAllBackground(MenuCart);
                    break;
                default:
                    MenuContact.Background = BackgroundSelected;
                    clearAllBackground(MenuContact);
                    break;
            }
        }
    }
}
