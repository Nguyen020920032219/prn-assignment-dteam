using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
