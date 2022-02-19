namespace QMarket.Api.Models
{
    public class Location{
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public decimal XCord { get; set; }
        public decimal YCord { get; set; }
        public int Rsid { get; set; }
    }
}