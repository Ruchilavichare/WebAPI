using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.API.Models
{
    public class Menu
    {
        public int MenuId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }
    }
}
