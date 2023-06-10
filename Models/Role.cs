using System;
using System.Collections.Generic;

namespace LearnHubRestaurent.Models;

public partial class Role
{
    public string RoleName { get; set; } = null!;

    public decimal Id { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
