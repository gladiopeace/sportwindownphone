using Com.IT.DiPaSport.View.BasePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Com.IT.DiPaSport.Model.UserControlManager
{
    public class UserControlManager : AbsUserControlManager
    {
        /// <summary>
        /// 
        /// </summary>
        public interface UserControlChangedListener
        {
            void OnUserControlAdded(BaseUserControl usercontrol);
            void OnuserControlRemoved(BaseUserControl usercontrol);
        }


        /// <summary>
        /// The on user control listener
        /// </summary>
        public UserControlChangedListener OnUserControlListener { private get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlManager"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public UserControlManager(UIElementCollection collection)
        {
            CurrentStackUserControl = collection;
        }

        /// <summary>
        /// Gets the current user control in stack.
        /// </summary>
        /// <returns></returns>
        public override BaseUserControl getCurrent()
        {
            if (CurrentStackUserControl.Count() == 0) return null;
            BaseUserControl current = CurrentStackUserControl.First() as BaseUserControl;
            return current;
        }

        /// <summary>
        /// Replaces the user control.
        /// </summary>
        /// <param name="newUserControl">The new user control.</param>
        public override void ReplaceUserControl(BaseUserControl newUserControl)
        {
            BaseUserControl current = getCurrent();
            if (current != null)
            {
                if (current.Name == newUserControl.Name) return;
            }
            CurrentStackUserControl.Clear();
            newUserControl.OnLoad();
            CurrentStackUserControl.Add(newUserControl);
            newUserControl.Visibility = System.Windows.Visibility.Visible;
            if (OnUserControlListener != null)
            {
                OnUserControlListener.OnUserControlAdded(newUserControl);
            }
        }

        public override void PushUserControl(BaseUserControl newUserControl)
        {
            BaseUserControl current = getCurrent();
            if (current != null)
            {
                current.Visibility = System.Windows.Visibility.Collapsed;
            }

            newUserControl.OnLoad();
            CurrentStackUserControl.Insert(0, newUserControl);
            if (OnUserControlListener != null)
            {
                OnUserControlListener.OnUserControlAdded(newUserControl);
            }
        }

        public override BaseUserControl PopUserControl()
        {
            if (CurrentStackUserControl.Count == 0) return null;
            CurrentStackUserControl.RemoveAt(0);
            if (CurrentStackUserControl.Count == 0)
            {
                return null;
            }
            else
            {
                BaseUserControl current = getCurrent();
                if (current == null) return null;
                current.OnLoad();
                current.Visibility = System.Windows.Visibility.Visible;
                return current;
            }
        }
    }
}
