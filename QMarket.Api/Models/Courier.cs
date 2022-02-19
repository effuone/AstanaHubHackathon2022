namespace QMarket.Api.Models
{
    public class Courier{
        public int CourierId { get; set; }
        public string CompanyName { get; set; }
        #nullable enable
        public string? ImageName { get; set; }
        #nullable disable
    }
}