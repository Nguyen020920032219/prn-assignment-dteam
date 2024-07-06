﻿using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class OrderService
{
    public int OrderServiceId { get; set; }

    public int? OrderId { get; set; }

    public int? ServiceId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? Price { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Service? Service { get; set; }
}
