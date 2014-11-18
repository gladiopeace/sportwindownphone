using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class LostPasswordModel : BaseModel
    {
        /// <summary>
        /// Losts the password.
        /// </summary>
        /// <param name="email">The email.</param>
        public void LostPassword(string email)
        {
            string url_lostpass = String.Format(Constants.URL_LOSTPASS, new string[] { email });
            Execute(url_lostpass);
        }
    }
}
