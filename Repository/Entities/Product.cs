using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int? StockQuantity { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CostOfGood> CostOfGoods { get; set; } = new List<CostOfGood>();

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
