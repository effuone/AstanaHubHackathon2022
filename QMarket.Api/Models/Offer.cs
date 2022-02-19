namespace QMarket.Api.Models
{
    public class Offer{
        public int OfferId { get; set; }
        public int CourierId { get; set; }
        public int OrderId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public decimal ExpectedDeliveryPrice { get; set; }
    }
}