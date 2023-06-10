using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHubRestaurent.Models;

public partial class Customerr
{

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? ImagePath { get; set; }

    public decimal Id { get; set; }

    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }


    public virtual ICollection<ProductCustomerr> ProductCustomerrs { get; set; } = new List<ProductCustomerr>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
