using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToyAPI.Models;

namespace ToyAPI.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ToyDbContext>();
                context.Database.Migrate(); // Apply pending migrations

                if (!context.Toys.Any()) // Check if the database is empty
                {
                    var toys = new List<Toy>
                    {
                        new Toy { Name = "Lego Creator", Price = 49.99M, Rating = 5, ReviewsCount = 250, IsBestSeller = true, AddedDate = DateTime.UtcNow.AddDays(-10), Category = "Building Toys", AffiliateLink = "https://affiliate.example.com/lego", DiscountedPrice = 39.99M, IsDealOfTheDay = false },
                        new Toy { Name = "Hot Wheels Track Set", Price = 29.99M, Rating = 4, ReviewsCount = 150, IsBestSeller = true, AddedDate = DateTime.UtcNow.AddDays(-20), Category = "Vehicle Toys", AffiliateLink = "https://affiliate.example.com/hotwheels", DiscountedPrice = 24.99M, IsDealOfTheDay = true },
                        new Toy { Name = "Barbie Dreamhouse", Price = 199.99M, Rating = 5, ReviewsCount = 500, IsBestSeller = true, AddedDate = DateTime.UtcNow.AddDays(-30), Category = "Dolls & Accessories", AffiliateLink = "https://affiliate.example.com/barbie", DiscountedPrice = 179.99M, IsDealOfTheDay = false },
                        new Toy { Name = "STEM Robot Kit", Price = 89.99M, Rating = 5, ReviewsCount = 300, IsBestSeller = false, AddedDate = DateTime.UtcNow.AddDays(-5), Category = "Best STEM Toys", AffiliateLink = "https://affiliate.example.com/robotkit", DiscountedPrice = 79.99M, IsDealOfTheDay = true }
                    };

                    context.Toys.AddRange(toys);
                    context.SaveChanges();
                }
            }
        }
    }
}
