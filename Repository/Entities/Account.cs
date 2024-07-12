using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Account
{
    public int AccountId { get; set; }

    public int? EmployeeId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Role { get; set; }

    public bool IsActive { get; set; }

    public virtual Employee? Employee { get; set; }
}
