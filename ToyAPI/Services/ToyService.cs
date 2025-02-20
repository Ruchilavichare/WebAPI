using ToyAPI.Models;

namespace ToyAPI.Services
{
    public class ToyService
    {
        public string GenerateLongTailKeyword(string baseKeyword, int year)
        {
            return $"Best {baseKeyword} {year}";
        }

        public string GenerateShortReview(Toy toy)
        {
            return $"{toy.Name} is a top-rated {toy.Category} toy with {toy.ReviewsCount} reviews and a {toy.Rating}-star rating. Buy now at {toy.AffiliateLink}";
        }
    }
}
