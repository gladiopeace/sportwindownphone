using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;

namespace Com.IT.DiPaSport.Model
{
    public class SearchByCategoryModel : BaseModel
    {

        public void GetListCategories()
        {
            Execute(Constants.URL_CATEGORIES + Constants.ENGLISH_VERSION);
        }
    }
}
