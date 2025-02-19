namespace ToyMVC.Models
{
    public class Toy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public int ReviewsCount { get; set; }
        public bool IsBestSeller { get; set; }
        public DateTime AddedDate { get; set; }
        public string Category { get; set; }
        public string AffiliateLink { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool IsDealOfTheDay { get; set; }
    }
}
