using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class TbCostofGood
{
    public int IdCostofGood { get; set; }

    public string? Costname { get; set; }

    public decimal? Cost { get; set; }

    public DateOnly? Date { get; set; }
}
