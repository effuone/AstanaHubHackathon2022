namespace QMarket.Api.Models
{
    public class Product{
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; } = null!;
        public decimal Weigth { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short? ModelYear { get; set; }
        public decimal ListPrice { get; set; }
    }
}