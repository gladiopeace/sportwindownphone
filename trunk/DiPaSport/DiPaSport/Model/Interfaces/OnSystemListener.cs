using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Model.Interfaces
{

    public interface DiPaListener
    {
        void OnBack();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface HomeActionListener : DiPaListener, OnRequestListener
    {
        /// <summary>
        /// Called when [take picture].
        /// </summary>
        void OnRequestProduct();

        /// <summary>
        /// Called when [search by qr code].
        /// </summary>
        void OnSearchByQRCode();

        /// <summary>
        /// Called when [search].
        /// </summary>
        /// <param name="keywork">The keywork.</param>
        void OnSearch(string keyword);

        /// <summary>
        /// Called when [product view].
        /// </summary>
        void OnProductView();

        /// <summary>
        /// Called when [open category].
        /// </summary>
        void OnOpenCategory();
        void OnBeginRequestCategory();

        /// <summary>
        /// Called when [close category].
        /// </summary>
        void OnCloseCategory();
        void OnEndRequestCategory();

        void OnLoginRequired();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface OnSearchByCategoryListener
    {
        /// <summary>
        /// Called when [search by category].
        /// </summary>
        /// <param name="catID">The cat identifier.</param>
        void OnSearchByCategory(string catID);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface OnProductsViewListener
    {
        /// <summary>
        /// Called when [detail open].
        /// </summary>
        /// <param name="product">The product.</param>
        void OnDetailOpen(ProductInfo product);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface OnRequestListener
    {
        /// <summary>
        /// Begins the loading.
        /// </summary>
        void StartRequest();

        /// <summary>
        /// Ends the loading.
        /// </summary>
        void EndRequest();
    }
}
