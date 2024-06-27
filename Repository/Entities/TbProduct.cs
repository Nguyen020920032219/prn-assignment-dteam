using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class TbProduct
{
    public int IdProduct { get; set; }

    public string? ProductName { get; set; }

    public string? Category { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? Description { get; set; }

    public int? IdCategory { get; set; }

    public virtual TbCategory? IdCategoryNavigation { get; set; }
}
