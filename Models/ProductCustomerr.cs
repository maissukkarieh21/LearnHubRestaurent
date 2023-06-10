using System;
using System.Collections.Generic;

namespace LearnHubRestaurent.Models;

public partial class ProductCustomerr
{
    public decimal? ProductId { get; set; }

    public decimal? CustomerId { get; set; }

    public decimal? Quantity { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public decimal Id { get; set; }

    public virtual Customerr? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
