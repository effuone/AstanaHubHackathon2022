namespace QMarket.Api.ViewModels
{
    public class OrderViewModel
    {
         public int OrderId { get; set; }
         public int CustomerId { get; set; }
         public DateTime OrderDate { get; set; }
         public DateTime ExpectedDate { get; set; }
         public int LocationIdFrom { get; set; }
         public int LocationIdTo { get; set; }
         public int NumberOfProducts { get; set; }
         public int ProductSum { get; set; }
         public decimal DeliveryPrice { get; set; }
    }
}