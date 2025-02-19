using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToyAPI.Models;

namespace ToyAPI.Data
{
    public class ToyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        public ToyDbContext(DbContextOptions<ToyDbContext> options) : base(options) { }
        public DbSet<Toy> Toys { get; set; }
    }
}
