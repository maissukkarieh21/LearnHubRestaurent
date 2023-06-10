using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHubRestaurent.Models;

public partial class Category
{
    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }
    public decimal Id { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
