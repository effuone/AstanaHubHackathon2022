namespace QMarket.Api.Models
{
    public class Order{
       public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public decimal ExpectedDeliveryPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StoreId { get; set; }
    }
}