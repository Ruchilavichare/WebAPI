using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
