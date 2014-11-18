using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseModel : AbsBaseModel
    {

        /// <summary>
        /// Registers the callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public JSONCallbackListener RegisterCallback { get; set; }

        /// <summary>
        /// Executes the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public override void Execute(string url)
        {
#if DEBUG
            Debug.WriteLine(url);
#endif
            var client = new RestClient() { BaseUrl = url };
            var request = new RestRequest(Method.GET);
            if (RegisterCallback != null)
            {
                RegisterCallback.StartRequest();
            }
            client.ExecuteAsync(request, response =>
            {
                if (response.ResponseStatus == ResponseStatus.Error || response.StatusCode == HttpStatusCode.NotFound)
                {
                    if (RegisterCallback != null)
                    {
                        RegisterCallback.OnErrors(-1, response.ErrorMessage);
                    }
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        if (RegisterCallback != null)
                        {
                            var jObject = JObject.Parse(response.Content);
                            RegisterCallback.OnResults(jObject);
                        }
                    }
                    catch (JsonReaderException ex)
                    {
                        if (RegisterCallback != null)
                        {
                            RegisterCallback.OnErrors(-1, ex.Message);
                        }
                    }
                    catch (WebException ex)
                    {
                        if (RegisterCallback != null)
                        {
                            RegisterCallback.OnErrors(-1, ex.Message);
                        }
                    }
                }

                if (RegisterCallback != null)
                {
                    RegisterCallback.EndRequest();
                }
            });
        } // END Execute
    }
}
