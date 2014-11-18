using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class Constants
    {
        private readonly static string HOST_TEST = "http://testdipacommerce.no-ip.org:8000/services/";
        private readonly static string HOST_PRIMARY = "http://www.dipacommerce.com/services/";

        // Current host for testing
#if DEBUG
        public readonly static string HOST = HOST_TEST;
#else
        public readonly static string HOST = HOST_PRIMARY;
#endif

        public readonly static string URL_CATEGORIES = HOST + "service.php?op=getListCate";
        public readonly static string URL_PRODUCT_FEATURES = HOST + "service.php?op=feartureproduct&userId={0}";
        public readonly static string URL_PRODUCT_BY_CATEGORY = HOST + "service.php?op=listProductBycate&category={0}&page={1}&idUser={1}";
        public readonly static string URL_LOGIN = HOST + "service.php?op=login&email={0}&password={1}";
        public readonly static string URL_LOSTPASS = HOST + "service.php?op=forgetpass&email_change={0}";
        public readonly static string URL_REGISTER = HOST + "service.php?op=register&custype={0}&firstname={1}&lastname={2}&email={3}&password={4}&company={5}&taxvat={6}" +
            "&street={7}&city={8}&region={9}&postcode={10}&country={11}&telephone={12}&fax={13}&newsletter=YES&client=WindowsPhone App";
        public readonly static string URL_SEACH = HOST + "service.php?op=searchPage&qsearch={0}&page={1}&idUser={2}";
        public readonly static string URL_SEARCH_BY_CATEGORY = HOST + "service.php?op=listProductBycate&category={0}&page={1}&idUser={2}";
        public readonly static string URL_COUPON = HOST + "service.php?op=getcounpon&couponCode={0}";
        public readonly static string URL_ORDER = HOST + "service.php?op=checkOut&email={0}&idUse={1}&order={2}";
        public readonly static string URL_PRODUCT_DETAIL = HOST + "service.php?op=getViewProduct&entityid={0}&idUser={1}";

        public readonly static string ENGLISH_VERSION = "&lang=en"; // Default Italian version

        public readonly static string EMAIL_ORDER = "order@dipasport.com";
        public readonly static string EMAIL_INFO = "info@dipasport.com";
        public readonly static string EMAIL_CONTACT = "contact@dipasport.com";

        public class SOCIAL
        {
            public readonly static string FACEBOOK = "https://facebook.com/dipasportsrl";
            public readonly static string TWITTER = "https://twitter.com/dipasport";
            public readonly static string YOUTUBE = "http://youtube.com/user/dipavideo";
            public readonly static string MAP = "https://www.google.com/maps/place/44%C2%B058%2718.4%22N+9%C2%B051%2718.1%22E/@44.9717755,9.855024,15z/data=!3m1!4b1!4m2!3m1!1s0x0:0x0";
        }

        /// <summary>
        /// 
        /// </summary>
        public class JSON_TAG
        {
            // Common
            public readonly static string ERROR_CODE = "ErrorCode";
            public readonly static string MESSAGE = "Message";
            public readonly static string DATA = "DATA";

            // Category
            public readonly static string CAT_ID = "id";
            public readonly static string CAT_TITLE = "title";
            public readonly static string CAT_SUBMENU = "submenu";

            /// <summary>
            /// 
            /// </summary>
            public class LOGIN
            {
                public readonly static string USER_ID = "userId";
                public readonly static string USER_NAME = "user";
                public readonly static string USER_GROUP_ID = "groupId";
                public readonly static string USER_COMPANY = "company";
                public readonly static string USER_TELEPHONE = "telephone";
                public readonly static string USER_EMAIL = "email";

                public static string ToString()
                {
                    return "loginSettings";
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public class PRODUCT_SEARCHING
            {
                public readonly static string PAGING = "Paging";
                public readonly static string NEXT_PAGE = "next-page";
            }

            public class PRODUCT_DETAIL
            {
                public readonly static String Id = "entity_id";
                public readonly static String Codicedipa = "codicedipa"; // product name
                // here
                public readonly static String Name = "name";
                public readonly static String Image = "image";
                public readonly static String SmallImage = "small_image";
                public readonly static String Thumbnail = "thumbnail";
                public readonly static String SpecialPrice = "special_price";
                public readonly static String Description = "description"; // don't used
                public readonly static String ShortDescription = "short_description"; // don't
                // used
                public readonly static String Status = "status";
                public readonly static String Images = "media_image";
                public readonly static String PartCondition = "condizionedelpezzo";
                public readonly static String CodeOem = "codicioem";
                public readonly static String Availability_note = "note";
                public readonly static String QuickOverview = "description";
                public readonly static String Suitablefor = "applicazione";
                public readonly static String PricePerCustomer = "prices_per_customer";
                public readonly static String UnAvailable = "disponibilenondisponibile";
                public readonly static String Available = "disponibile";
                public readonly static String RefCodes = "codicidiriferimentonascosto";
                public readonly static String Compatibility = "compatibile";

                // Price by customer group
                public class PRICE
                {
                    public readonly static String NORMALE = "price";
                    public readonly static String OFFICINA = "msrp";
                    public readonly static String RICAMBISTA = "prezzoconsrivenditori";
                    public readonly static String RICAMBISTA_A = RICAMBISTA;
                    public readonly static String GROSSISTA = "prezzoconsgrossisti";
                    public readonly static String GROSSISTA_A = GROSSISTA;
                    public readonly static String OFF_S = "tier_price";
                    public readonly static String PURCHASE_PRICE = "prezzodiacquisto";
                    public readonly static String COMPETITIVE_PRICE = "prezziconcorrenti";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Dimens
        {
            public readonly static int SCREEN_HEIGHT = 768;
        }

        /// <summary>
        /// 
        /// </summary>
        public class ScreensName
        {
            public readonly static string DETAIL = "ProductDetailsViewUserControl";

            #region Home
            public readonly static string HOME = "HomeViewUserControl";
            public readonly static string PRODUCT_REQUEST = "ProductRequestViewUserControl";
            #endregion

            #region Search
            public readonly static string SEARCH = "SearchViewUserControl";
            public readonly static string SEARCH_NOT_FOUND = "SearchNotFoundViewUserControl";
            #endregion

            #region Cart
            public readonly static string CART = "CartViewUserControl";
            public readonly static string CART_QUANTITY = "CartQuantityViewUserControl";
            #endregion

            #region Contact Page
            public readonly static string CONTACT = "ContactViewUserControl";
            public readonly static string SOCIAL = "SocialViewUserControl";
            #endregion

            #region Account Page
            public readonly static string ACCOUNT = "AccountViewUserControl";
            public readonly static string LOGOUT = "LogoutViewUserControl";
            public readonly static string CUSTOMER_TYPE = "CustomerTypeViewUserControl";
            public readonly static string COUNTRIES = "CountriesViewUserControl";
            public readonly static string REGISTER = "RegisterViewUserControl";

            #endregion
        }
    }
}
