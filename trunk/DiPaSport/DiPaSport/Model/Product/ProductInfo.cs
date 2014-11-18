using Com.IT.DiPaSport.Model.Customer;
using Com.IT.DiPaSport.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model
{
    public class ProductInfo : ProductTable
    {

        /// <summary>
        /// The _description
        /// </summary>
        private string _description = String.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// The _short desc
        /// </summary>
        private string _shortDesc = String.Empty;
        public string ShortDescription
        {
            get
            {
                return _shortDesc;
            }
            set
            {
                _shortDesc = value;
            }
        }

        /// <summary>
        /// The _availability
        /// </summary>
        private string _availability = String.Empty;
        public string Availability
        {
            get
            {
                return _availability;
            }
            set
            {
                _availability = value;
            }
        }

        /// <summary>
        /// Gets the availability with format.
        /// </summary>
        /// <value>
        /// The availability with format.
        /// </value>
        public string AvailabilityWithFormat
        {
            get
            {
                return string.Format("<b>{0}:</b> {1}", AppResources.DetailAvaiability, Availability);
            }
        }

        /// <summary>
        /// The _quick overview
        /// </summary>
        private string _quickOverview = String.Empty;
        public string QuickOverview
        {
            get
            {
                return _quickOverview;
            }
            set
            {
                _quickOverview = value;
            }
        }

        /// <summary>
        /// Gets the quick overview with format.
        /// </summary>
        /// <value>
        /// The quick overview with format.
        /// </value>
        public string QuickOverviewWithFormat
        {
            get
            {
                return string.Format("<b>{0}:</b> {1}", AppResources.DetailQuickOverView, QuickOverview);
            }
        }

        /// <summary>
        /// The _suitable for
        /// </summary>
        private string _suitableFor = String.Empty;
        public string SuitableFor
        {
            get
            {
                return _suitableFor;
            }
            set
            {
                _suitableFor = value;
            }
        }

        /// <summary>
        /// Gets the suitable for with format.
        /// </summary>
        /// <value>
        /// The suitable for with format.
        /// </value>
        public string SuitableForWithFormat
        {
            get
            {
                return string.Format("<b>{0}:</b> {1}", AppResources.DetailSuiablerFor, SuitableFor);
            }
        }

        /// <summary>
        /// The _part condition
        /// </summary>
        private string _partCondition = String.Empty;
        public string PartCondition
        {
            get
            {
                return _partCondition;
            }
            set
            {
                _partCondition = value;
            }
        }
        public string PartConditionWithFormat
        {
            get
            {
                return string.Format("<b>{0}:</b> {1}", AppResources.DetailPartCondition, PartCondition);
            }
        }

        /// <summary>
        /// The _suggest price
        /// </summary>
        private string _suggestPrice = String.Empty;
        public string SuggestPrice
        {
            get
            {
                return _suggestPrice;
            }
            set
            {
                _suggestPrice = value;
            }
        }

        /// <summary>
        /// The _refs code
        /// </summary>
        private string _refsCode = String.Empty;
        public string RefsCode
        {
            get
            {
                return _refsCode;
            }
            set
            {
                _refsCode = value;
            }
        }

        /// <summary>
        /// Gets the refs code with format.
        /// </summary>
        /// <value>
        /// The refs code with format.
        /// </value>
        public string RefsCodeWithFormat
        {
            get
            {
                return string.Format("<b>{0}:</b> {1}", AppResources.DetailRefsCode, RefsCode);
            }
        }
        /// <summary>
        /// The _compatibility
        /// </summary>
        private string _compatibility = String.Empty;
        public string Compatibility
        {
            get
            {
                return _compatibility;
            }
            set
            {
                _compatibility = value;
            }
        }

        /// <summary>
        /// Gets the compatibility with format.
        /// </summary>
        /// <value>
        /// The compatibility with format.
        /// </value>
        public string CompatibilityWithFormat
        {
            get
            {
                return string.Format("<b>{0}:</b> {1}", AppResources.DetailCompatibility, Compatibility);
            }
        }


        /// <summary>
        /// Gets or sets the raw json.
        /// </summary>
        /// <value>
        /// The raw json.
        /// </value>
        public JObject RawJSON { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public List<string> Images { get; set; }

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <value>
        /// The HTML.
        /// </value>
        public string HtmlPrice
        {
            get
            {

                string HtmlSrc = "<html><head><meta name=\"viewport\" content=\"user-scalable=no\"/></head><body bgcolor=\"#FFFFFF\" style=\"margin:0;padding:0;font-size:20px\">";
                HtmlSrc += string.Format("{0}<br><b>{1}</b>", Name, CodeWithFormat);
                if (LoginModel.IsLogin)
                {
                    int gid = LoginModel.RetrieveLoginSettings().UserGroup;
                    if (gid == CustomerGroup.NOPREZZI)
                    {
                        HtmlSrc += string.Format("<br><font color=\"red\">{0}</font>", AppResources.DetailPriceUnAvailable);
                    }
                    else if (gid == CustomerGroup.BANNATI)
                    {
                        HtmlSrc += string.Format("<br><font color=\"red\">{0}</font>", AppResources.DetailCustomerBanned);
                    }
                    else
                    {
                        HtmlSrc += string.Format("<br><font color=\"red\">{0}</font>", PriceWithFormat);
                        JToken tmp = null;
                        if (gid == CustomerGroup.RICAMBISTA_A) // (GROUP ID 13) add suggest price for garage shop
                        {
                            // OFFICINA
                            RawJSON.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA, out tmp);
                            float officinaPrice = tmp != null ? float.Parse(RawJSON[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA].ToString(), CultureInfo.InvariantCulture.NumberFormat) : .0f;
                            string officinaSuggestPrice = string.Format("<br><b>{0}</b> &#8364;{1:0.00}", AppResources.DetailPriceGarageShop, officinaPrice);
                            HtmlSrc += officinaSuggestPrice;
                        }
                        else if (gid == CustomerGroup.GROSSISTA_A)
                        {
                            // OFFICINA
                            RawJSON.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA, out tmp);
                            float officinaPrice = tmp != null ? float.Parse(RawJSON[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA].ToString(), CultureInfo.InvariantCulture.NumberFormat) : .0f;
                            string officinaSuggestPrice = string.Format("<br><b>{0}</b> &#8364;{1:0.00}", AppResources.DetailPriceGarageShop, officinaPrice);
                            // RICAMBISTA_A
                            RawJSON.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA, out tmp);
                            float ricambistaPrice = tmp != null ? float.Parse(RawJSON[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.RICAMBISTA_A].ToString(), CultureInfo.InvariantCulture.NumberFormat) : .0f;
                            string ricambistaSuggestPrice = string.Format("<br><b>{0}</b> &#8364;{1:0.00}", AppResources.DetailPriceRetailer, ricambistaPrice);
                            HtmlSrc += string.Format("{0}{1}", officinaSuggestPrice, ricambistaSuggestPrice);
                        }
                        else if (gid == CustomerGroup.AMMINISTRATORE)
                        {
                            // OFFICINA
                            RawJSON.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA, out tmp);
                            float officinaPrice = tmp != null ? float.Parse(RawJSON[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA].ToString(), CultureInfo.InvariantCulture.NumberFormat) : .0f;
                            string officinaSuggestPrice = string.Format("<br><b>{0}</b> &#8364;{1:0.00}", AppResources.DetailPriceGarageShop, officinaPrice);

                            // RICAMBISTA_A
                            RawJSON.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.OFFICINA, out tmp);
                            float ricambistaPrice = tmp != null ? float.Parse(RawJSON[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.RICAMBISTA_A].ToString(), CultureInfo.InvariantCulture.NumberFormat) : .0f;
                            string ricambistaSuggestPrice = string.Format("<br><b>{0}</b> &#8364;{1:0.00}", AppResources.DetailPriceRetailer, ricambistaPrice);

                            // GROSSISTA + A
                            RawJSON.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.GROSSISTA_A, out tmp);
                            float grossitaAPrice = tmp != null ? float.Parse(RawJSON[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.GROSSISTA_A].ToString(), CultureInfo.InvariantCulture.NumberFormat) : .0f;
                            string grossitaASuggestPrice = string.Format("<br><b>{0}</b> &#8364;{1:0.00}", AppResources.DetailPriceDistributor, grossitaAPrice);

                            // PURCHASE_PRICE
                            RawJSON.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.PURCHASE_PRICE, out tmp);
                            float purchasePrice = tmp != null ? float.Parse(RawJSON[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.PURCHASE_PRICE].ToString(), CultureInfo.InvariantCulture.NumberFormat) : .0f;
                            string purchaseSuggestPrice = string.Format("<br><b>{0}</b> &#8364;{1:0.00}", AppResources.DetailPurchsePrice, purchasePrice);

                            // COMPETITIVE_PRICE
                            RawJSON.TryGetValue(Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.COMPETITIVE_PRICE, out tmp);
                            string competitive = tmp != null ? RawJSON[Constants.JSON_TAG.PRODUCT_DETAIL.PRICE.COMPETITIVE_PRICE].ToString() : string.Empty;
                            string competitiveSuggest = string.Format("<br><b>{0}</b><br>{1}", AppResources.DetailCompetitivePrice, competitive);
                            competitiveSuggest = competitiveSuggest.Replace("\r\n", "<br>");

                            HtmlSrc += string.Format("{0}{1}{2}{3}{4}", officinaSuggestPrice, ricambistaSuggestPrice, grossitaASuggestPrice, purchaseSuggestPrice, competitiveSuggest);
                        }
                    }
                    
                }
                HtmlSrc = HtmlSrc.Replace("€", "&#8364;");
                HtmlSrc += "</body></html>";
                return HtmlSrc;
            }
        }

        /// <summary>
        /// Gets the HTML detail.
        /// </summary>
        /// <value>
        /// The HTML detail.
        /// </value>
        public string HtmlDetail
        {
            get
            {
                string HtmlSrc = "<html><head><meta name=\"viewport\" content=\"user-scalable=no\"/></head><body bgcolor=\"#FFFFFF\" style=\"margin:0;padding:5;font-size:14px\">";
                HtmlSrc += string.Format("{0}<br>", CodeOEMWithFormat);
                HtmlSrc += string.Format("{0}<br>", PartConditionWithFormat);
                HtmlSrc += string.Format("{0}<br>", AvailabilityWithFormat);
                HtmlSrc += string.Format("{0}<br>", QuickOverviewWithFormat);
                HtmlSrc += string.Format("{0}<br>", SuitableForWithFormat);
                if (LoginModel.Group == CustomerGroup.AMMINISTRATORE)
                {
                    HtmlSrc += string.Format("{0}<br>", RefsCodeWithFormat);
                    HtmlSrc += string.Format("{0}<br>", CompatibilityWithFormat);
                }
                HtmlSrc += "</body></html>";
                return HtmlSrc;
            }
        }
    }
}
