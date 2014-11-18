using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using Com.IT.DiPaSport.Model;

namespace Com.IT.DiPaSport.Database
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DiPaSportDB : DataContext
    {
        private readonly static string Connection = "Data Source=isostore:/DiPaSport.sdf";

        /// <summary>
        /// Initializes a new instance of the <see cref="DiPaSportDB"/> class.
        /// </summary>
        public DiPaSportDB() : base(Connection)
        {
            
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public Table<ProductTable> Products
        {
            get
            {
                return this.GetTable<ProductTable>();
            }
        }
    }
}
