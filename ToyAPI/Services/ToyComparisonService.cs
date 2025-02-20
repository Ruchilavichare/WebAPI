using ToyAPI.Models;

namespace ToyAPI.Services
{
    public class ToyComparisonService
    {
        public string GenerateComparisonTable(List<Toy> toys)
        {
            string table = "<table border='1'><tr><th>Name</th><th>Price</th><th>Rating</th><th>Reviews</th><th>Affiliate Link</th></tr>";
            foreach (var toy in toys)
            {
                table += $"<tr><td>{toy.Name}</td><td>${toy.Price}</td><td>{toy.Rating}⭐</td><td>{toy.ReviewsCount}</td><td><a href='{toy.AffiliateLink}' target='_blank'>Buy Now</a></td></tr>";
            }
            table += "</table>";
            return table;
        }
    }
}
