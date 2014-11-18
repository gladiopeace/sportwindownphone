using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginObject
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public int UserGroup { get; set; }
        public string UserCompany { get; set; }
        public string UserTelephone { get; set; }
        public string UserEmail { get; set; }
    }
}
