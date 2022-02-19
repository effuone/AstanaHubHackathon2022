namespace QMarket.Api.Models
{
    public class OrderedLocation{
        public int OrderedLocationId { get; set; }
        public int OrderId { get; set; }
        public int LocationIdFrom { get; set; }
        public int LocationIdTo { get; set; }
    }
}