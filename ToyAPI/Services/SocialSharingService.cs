using ToyAPI.Models;

namespace ToyAPI.Services
{
    public class SocialSharingService
    {
        public string GeneratePinterestPin(Toy toy)
        {
            return $"Check out {toy.Name}! A must-have {toy.Category} toy. Get yours here: {toy.AffiliateLink}";
        }
    }
}
