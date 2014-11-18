using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public abstract class AbsBaseModel
    {
        /// <summary>
        /// Executes the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public virtual void Execute(string url)
        {

        }

        /// <summary>
        /// Called when [begin execute].
        /// </summary>
        protected virtual void OnBeginExecute()
        {

        }

        /// <summary>
        /// Called when [end execute].
        /// </summary>
        protected virtual void OnEndExecute()
        {

        }
    }
}
