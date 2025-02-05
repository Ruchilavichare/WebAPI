using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public int? MenuId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Menu? Menu { get; set; }

    public virtual Order? Order { get; set; }
}
