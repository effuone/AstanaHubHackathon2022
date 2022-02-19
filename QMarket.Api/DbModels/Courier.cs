using System;
using System.Collections.Generic;

namespace QMarket.Api.DbModels
{
    public partial class Courier
    {
        public Courier()
        {
            Offers = new HashSet<Offer>();
        }

        public int CourierId { get; set; }
        public string CompanyName { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
