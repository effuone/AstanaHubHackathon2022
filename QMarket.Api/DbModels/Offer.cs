using System;
using System.Collections.Generic;

namespace QMarket.Api.DbModels
{
    public partial class Offer
    {
        public int OfferId { get; set; }
        public int CourierId { get; set; }
        public int OrderId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public decimal ExpectedDeliveryPrice { get; set; }

        public virtual Courier Courier { get; set; }
        public virtual Order Order { get; set; }
    }
}
