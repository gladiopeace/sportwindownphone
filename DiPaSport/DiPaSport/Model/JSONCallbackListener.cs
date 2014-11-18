using Com.IT.DiPaSport.Model.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.IT.DiPaSport.Model
{
    /// <summary>
    /// Callback to receive loaded data.
    /// </summary>
    public abstract class JSONCallbackListener : OnRequestListener
    {
        /// <summary>
        /// Called when download and convert to JObject successful.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="tag">The tag.</param>
        public virtual void OnResults(JObject results){}

        /// <summary>
        /// Called when [results] with attached an object
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="tag">The tag.</param>
        public virtual void OnResults(JObject results, object tag) { }

        /// <summary>
        /// Call when errors related connection or convert to JObject fail.
        /// </summary>
        /// <param name="ErrorCode">The error code.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <param name="tag">The tag.</param>
        public virtual void OnErrors(sbyte ErrorCode, string ErrorMessage) { }

        /// <summary>
        /// Called when [errors] with attach an object
        /// </summary>
        /// <param name="ErrorCode">The error code.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <param name="tag">The tag.</param>
        public virtual void OnErrors(sbyte ErrorCode, string ErrorMessage, object tag) { }

        /// <summary>
        /// Begins the loading.
        /// </summary>
        public virtual void StartRequest(){ }

        /// <summary>
        /// Ends the loading.
        /// </summary>
        public virtual void EndRequest(){ }
    }
}
