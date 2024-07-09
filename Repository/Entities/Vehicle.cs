using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public int? CustomerId { get; set; }

    public string? LicensePlate { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
