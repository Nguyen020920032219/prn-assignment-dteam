using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class TbCustomer
{
    public int IdCustomer { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Camo { get; set; }

    public string? Camodel { get; set; }

    public string? Address { get; set; }

    public int? Points { get; set; }

    public int? IdVehicleType { get; set; }

    public virtual TbVehicleType? IdVehicleTypeNavigation { get; set; }

    public virtual ICollection<TbCash> TbCashes { get; set; } = new List<TbCash>();
}
