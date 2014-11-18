using Com.IT.DiPaSport.Resources;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    [Table]
    public class ProductTable
    {

        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int AI { get; set; }
        /// <summary>
        /// The _code
        /// </summary>
        private string _code = String.Empty;
        [Column()]
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        /// <summary>
        /// Gets the code with format.
        /// </summary>
        /// <value>
        /// The code with format.
        /// </value>
        public string CodeWithFormat
        {
            get
            {
                return AppResources.ProductCode + _code;
            }
        }

        /// <summary>
        /// The _userid
        /// </summary>
        private string _userid = String.Empty;
        [Column()]
        public string UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }
        }

        /// <summary>
        /// The _name
        /// </summary>
        private string _name = String.Empty;
        [Column()]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// The _price
        /// </summary>
        private float _price = .0f;
        [Column()]
        public float Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }

        /// <summary>
        /// Gets or sets the price with format.
        /// </summary>
        /// <value>
        /// The price with format.
        /// </value>
        public string PriceWithFormat
        {
            get
            {
                string HtmlSrc = "<html><head><meta name=\"viewport\" content=\"user-scalable=no\"/></head><body bgcolor=\"#FFFFFF\" style=\"margin:0;padding:0;font-size:18px;color:red;overflow:hidden\">";
                if (SpecialPrice > 0)
                {
                    HtmlSrc += string.Format("{0} <strike>&#8364;{1:0.00}</strike> {2}{3:0.00} {4}", AppResources.ProductPricePrefix, Price, AppResources.DetailSpecialPrice, SpecialPrice, AppResources.ProductPriceSuffix);
                }
                else
                {
                    HtmlSrc += string.Format("{0} &#8364;{1:0.00} {2}", AppResources.ProductPricePrefix, Price, AppResources.ProductPriceSuffix);
                }
                HtmlSrc += "</body></html>";
                return HtmlSrc;
            }
        }

        /// <summary>
        /// Gets the price with special price.
        /// </summary>
        /// <value>
        /// The price with special price.
        /// </value>
        public string PriceWithSpecialPrice
        {
            get
            {
                if (SpecialPrice > 0)
                {
                    return string.Format("{0} €{1:0.00} {2}", AppResources.ProductPricePrefix, SpecialPrice, AppResources.ProductPriceSuffix);
                }
                else
                {
                    return string.Format("{0} €{1:0.00} {2}", AppResources.ProductPricePrefix, Price, AppResources.ProductPriceSuffix);
                }
            }
        }
        /// <summary>
        /// The _image path
        /// </summary>
        private string _imagePath = String.Empty;
        [Column()]
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
            }
        }

        /// <summary>
        /// The _quantity
        /// </summary>
        private int _quantity = 0;
        [Column()]
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }

        /// <summary>
        /// Gets the quantity with format.
        /// </summary>
        /// <value>
        /// The quantity with format.
        /// </value>
        public string QuantityWithFormat
        {
            get
            {
                return String.Format("{0}: {1}", AppResources.CartQuantity, _quantity);
            }
        }

        /// <summary>
        /// The _id
        /// </summary>
        private string _id = String.Empty;
        [Column(IsPrimaryKey = true)]
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// The _special price
        /// </summary>
        private float _specialPrice = .0f;
        [Column()]
        public float SpecialPrice
        {
            get
            {
                return _specialPrice;
            }
            set
            {
                _specialPrice = value;
            }
        }

        /// <summary>
        /// The _code oem
        /// </summary>
        private string _codeOem = String.Empty;
        public string CodeOEM
        {
            get
            {
                return _codeOem;
            }
            set
            {
                _codeOem = value;
            }
        }

        /// <summary>
        /// Gets the code oem with format.
        /// </summary>
        /// <value>
        /// The code oem with format.
        /// </value>
        public string CodeOEMWithFormat
        {
            get
            {
                return string.Format("<b>{0}</b> {1}", AppResources.ProductCodeOEM, CodeOEM);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is logined.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logined; otherwise, <c>false</c>.
        /// </value>
        public bool IsLogined
        {
            get
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(Constants.JSON_TAG.LOGIN.ToString()))
                {
                    return true;
                }
                return false;
            }
        }

    }
}
