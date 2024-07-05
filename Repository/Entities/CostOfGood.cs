using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class CostOfGood
{
    public int CostOfGoodId { get; set; }

    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Product? Product { get; set; }
}
