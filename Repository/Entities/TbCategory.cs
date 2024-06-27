using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class TbCategory
{
    public int IdCategory { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<TbProduct> TbProducts { get; set; } = new List<TbProduct>();
}
