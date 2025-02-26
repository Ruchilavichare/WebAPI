using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models;

public partial class Order
{
    public int OrderId { get; set; }

    [Required(ErrorMessage = "Customer is required")]
    public int? CustomerId { get; set; } // Nullable to enforce required validation

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
