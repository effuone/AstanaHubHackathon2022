using System;
using System.Collections.Generic;

namespace QMarket.Api.DbModels
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            Stocks = new HashSet<Stock>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string ImageName { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
