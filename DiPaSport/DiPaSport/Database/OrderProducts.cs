using Com.IT.DiPaSport.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Com.IT.DiPaSport.Database
{
    public class OrderProducts
    {
        private static DiPaSportDB DB;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderProducts"/> class.
        /// </summary>
        public OrderProducts()
        {
            DB = new DiPaSportDB();
            if (!DB.DatabaseExists())
            {
                DB.CreateDatabase();
            }
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public IList<ProductTable> GetProducts(string userid)
        {
            List<ProductTable> products = new List<ProductTable>();
            using (var db = new DiPaSportDB())
            {
                var query = from p in db.Products 
                            where p.UserID == userid
                            orderby p.AI descending
                            select p;
                products = query.ToList();
            }
            return products;
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public bool AddProduct(ProductTable product)
        {
            try
            {
                using (var db = new DiPaSportDB())
                {
                    db.Products.InsertOnSubmit(product);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified identifier is exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool IsExist(string id)
        {
            bool isExist = false;

            using (var db = new DiPaSportDB())
            {
                if (db.Products.Count() == 0) return false;
                var query = from p in db.Products
                            where p.ID == id
                            orderby p.AI descending
                            select p;
                isExist = query.Count() > 0 ? true : false;
            }
            return isExist;
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void DeleteProduct(ProductTable product)
        {
            using (var db = new DiPaSportDB())
            {
                db.Products.Attach(product);
                db.Products.DeleteOnSubmit(product);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="entityID">The entity identifier.</param>
        public bool DeleteProduct(string entityID)
        {
            using (var db = new DiPaSportDB())
            {
                var existingProduct = db.Products.Single(p => p.ID == entityID);
                if (existingProduct == null) return false;
                db.Products.DeleteOnSubmit(existingProduct);
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// Deletes all products.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        public bool DeleteAllProducts(string userID)
        {
            using (var db = new DiPaSportDB())
            {
                IEnumerable<ProductTable> productsToDel = (from p in db.Products where p.UserID == userID select p).ToList();
                db.Products.DeleteAllOnSubmit(productsToDel);
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the quantity.
        /// </summary>
        /// <param name="entityID">The entity identifier.</param>
        /// <param name="quantity">The quantity.</param>
        public void UpdateQuantity(string entityID, int quantity)
        {
            using (var db = new DiPaSportDB())
            {
                IQueryable<ProductTable> pQuery = from p in db.Products where p.ID == entityID select p;
                ProductTable product = pQuery.FirstOrDefault();
                product.Quantity = quantity;
                db.SubmitChanges();
            }
        }
    }
}
