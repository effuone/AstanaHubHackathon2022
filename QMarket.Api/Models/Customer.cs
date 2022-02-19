namespace QMarket.Api.Models
{
    public class Customer{
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        #nullable enable
        public string? ImageName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
         #nullable disable
        public int LocationId { get; set; }
    }
}