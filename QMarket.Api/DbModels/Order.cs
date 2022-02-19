using System;
using System.Collections.Generic;

namespace QMarket.Api.DbModels
{
    public partial class Order
    {
        public Order()
        {
            Offers = new HashSet<Offer>();
            OrderItems = new HashSet<OrderItem>();
            OrderedLocations = new HashSet<OrderedLocation>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public decimal ExpectedDeliveryPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StoreId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderedLocation> OrderedLocations { get; set; }
    }
}
