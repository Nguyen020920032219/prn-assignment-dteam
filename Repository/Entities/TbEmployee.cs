using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class TbEmployee
{
    public int IdEmployee { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Role { get; set; }

    public decimal? Salary { get; set; }

    public string? Password { get; set; }
}
