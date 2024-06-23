using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class TbService
{
    public int IdService { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<TbCash> TbCashes { get; set; } = new List<TbCash>();
}
