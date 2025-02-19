using HotelManagementSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace HotelManagementSystem.API.Data
{
    public class HotelManagementDBContext : DbContext
    {
        public HotelManagementDBContext(DbContextOptions<HotelManagementDBContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
    }
}
