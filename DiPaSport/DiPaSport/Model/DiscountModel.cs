using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class DiscountModel : BaseModel
    {
        public void CheckCouponCode(string couponCode)
        {
            string url = string.Format(Constants.URL_COUPON, couponCode);
            Execute(url);
        }

        /// <summary>
        /// Validates the code.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static bool ValidateCode(JObject json)
        {
            string from_date = (string)json["from_date"];
            string to_date = (string)json["to_date"];
            DateTime from = DateTime.ParseExact(from_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(to_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime current = DateTime.Now;
            return (current >= from && current <= to);
        }

        /// <summary>
        /// Determines whether the specified json is used.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static bool IsUsed(JObject json)
        {
            int apply = (int)json["apply"];
            return apply > 0;
        }
    }
}
