using System;
using System.Collections.Generic;

namespace QMarket.Api.DbModels
{
    public partial class Stock
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
