using System;
using System.Collections.Generic;

namespace QMarket.Api.DbModels
{
    public partial class Location
    {
        public Location()
        {
            OrderedLocationLocationIdFromNavigations = new HashSet<OrderedLocation>();
            OrderedLocationLocationIdToNavigations = new HashSet<OrderedLocation>();
            Stores = new HashSet<Store>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public decimal XCord { get; set; }
        public decimal YCord { get; set; }
        public int Rsid { get; set; }

        public virtual ICollection<OrderedLocation> OrderedLocationLocationIdFromNavigations { get; set; }
        public virtual ICollection<OrderedLocation> OrderedLocationLocationIdToNavigations { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
