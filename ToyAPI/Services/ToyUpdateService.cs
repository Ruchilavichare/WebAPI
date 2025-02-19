using ToyAPI.Data;
using ToyAPI.Models;

namespace ToyAPI.Services
{
    public class ToyUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _updateInterval = TimeSpan.FromDays(1); // Set update frequency

        public ToyUpdateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ToyDbContext>();
                    await UpdateToyListings(context);
                }
                await Task.Delay(_updateInterval, stoppingToken);
            }
        }

        private async Task UpdateToyListings(ToyDbContext context)
        {
            // Simulate fetching fresh toy data (e.g., from an external API)
            var newToy = new Toy
            {
                Name = "New Hot Toy",
                Price = 29.99M,
                Rating = 5,
                ReviewsCount = 100,
                IsBestSeller = true,
                AddedDate = DateTime.UtcNow,
                Category = "Best STEM Toys",
                AffiliateLink = "https://affiliate.example.com/new-hot-toy",
                DiscountedPrice = 24.99M,
                IsDealOfTheDay = true
            };
            context.Toys.Add(newToy);
            await context.SaveChangesAsync();
        }
    }
}
