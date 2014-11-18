using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class RegisterModel : BaseModel
    {
        public void Register(Dictionary<string, string> registerInfo)
        {
            string url_register = String.Format(Constants.URL_REGISTER, new string[] {
                registerInfo["custype"],
                registerInfo["fname"],
                registerInfo["lname"],
                registerInfo["email"],
                registerInfo["pass"],
                registerInfo["company"],
                registerInfo["tax"],
                registerInfo["address"],
                registerInfo["city"],
                registerInfo["region"],
                registerInfo["postcode"],
                registerInfo["country"],
                registerInfo["tel"],
                registerInfo["fax"],
            });

            url_register = url_register.Replace(" ", "%20").Replace("|", "%7C");

            Execute(url_register);
        }
    }
}
