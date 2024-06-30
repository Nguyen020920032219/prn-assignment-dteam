using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class TbVehicleType
{
    public int IdVehicleType { get; set; }

    public string? Name { get; set; }

    public int? Class { get; set; }

    public virtual ICollection<TbCash> TbCashes { get; set; } = new List<TbCash>();

    public virtual ICollection<TbCustomer> TbCustomers { get; set; } = new List<TbCustomer>();
}
