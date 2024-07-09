using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class OrderProduct
{
    public int OrderProductsId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
