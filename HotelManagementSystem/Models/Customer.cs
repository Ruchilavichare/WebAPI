using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Invalid phone number")]
    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
