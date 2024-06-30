using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class TbCash
{
    public int IdCash { get; set; }

    public string? Transno { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }

    public int? IdVehicleType { get; set; }

    public int? IdService { get; set; }

    public int? IdCustomer { get; set; }

    public virtual TbCustomer? IdCustomerNavigation { get; set; }

    public virtual TbService? IdServiceNavigation { get; set; }

    public virtual TbVehicleType? IdVehicleTypeNavigation { get; set; }
}
