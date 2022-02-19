namespace QMarket.Api.Models
{
    public class Store{
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        #nullable enable
        public string? ImageName { get; set; }
        #nullable disable
        public int LocationId { get; set; }
    }
}