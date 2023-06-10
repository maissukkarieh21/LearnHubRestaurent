using System;
using System.Collections.Generic;

namespace LearnHubRestaurent.Models;

public partial class UserLogin
{
    public string? UserName { get; set; }

    public string? Password { get; set; }

    public decimal? RoleId { get; set; }

    public decimal Id { get; set; }

    public decimal? CustomerId { get; set; }

    public virtual Customerr? Customer { get; set; }

    public virtual Role? Role { get; set; }
}
