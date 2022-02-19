using System;
using System.Collections.Generic;

namespace QMarket.Api.DbModels
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            Stocks = new HashSet<Stock>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; }
        public decimal Weigth { get; set; }
        public int CategoryId { get; set; }
        public short? ModelYear { get; set; }
        public decimal ListPrice { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
