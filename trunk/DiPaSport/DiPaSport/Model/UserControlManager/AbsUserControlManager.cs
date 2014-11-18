using Com.IT.DiPaSport.View.BasePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Com.IT.DiPaSport.Model.UserControlManager
{
    public abstract class AbsUserControlManager
    {
        /// <summary>
        /// The current stack user control
        /// </summary>
        protected UIElementCollection CurrentStackUserControl;
        
        /// <summary>
        /// Gets the current user control in stack.
        /// </summary>
        /// <returns></returns>
        public virtual BaseUserControl getCurrent() { return null; }

        /// <summary>
        /// Replaces the user control.
        /// </summary>
        /// <param name="newUserControl">The new user control.</param>
        public virtual void ReplaceUserControl(BaseUserControl newUserControl) { }

        /// <summary>
        /// Pushes the user control.
        /// </summary>
        /// <param name="newUserControl">The new user control.</param>
        public virtual void PushUserControl(BaseUserControl newUserControl) { }

        /// <summary>
        /// Pops the user control.
        /// </summary>
        /// <returns></returns>
        public virtual BaseUserControl PopUserControl() { return null; }
        
        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public virtual int Count() { return CurrentStackUserControl.Count(); }
    }
}
