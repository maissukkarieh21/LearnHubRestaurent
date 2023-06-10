using System;
using System.Collections.Generic;

namespace LearnHubRestaurent.Models;

public partial class Product
{
    public string? Namee { get; set; }

    public decimal? Sale { get; set; }

    public decimal? Price { get; set; }

    public decimal? CategoryId { get; set; }

    public decimal Id { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ProductCustomerr> ProductCustomerrs { get; set; } = new List<ProductCustomerr>();
}
