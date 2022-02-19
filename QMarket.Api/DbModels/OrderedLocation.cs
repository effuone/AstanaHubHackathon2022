using System;
using System.Collections.Generic;

namespace QMarket.Api.DbModels
{
    public partial class OrderedLocation
    {
        public int OrderedLocationId { get; set; }
        public int OrderId { get; set; }
        public int LocationIdFrom { get; set; }
        public int LocationIdTo { get; set; }

        public virtual Location LocationIdFromNavigation { get; set; }
        public virtual Location LocationIdToNavigation { get; set; }
        public virtual Order Order { get; set; }
    }
}
