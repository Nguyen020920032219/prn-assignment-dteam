using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public string TransactionNo { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? VehicleId { get; set; }

    public int? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual Vehicle? Vehicle { get; set; }
}
