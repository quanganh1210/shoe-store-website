using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class Category
{
    public long CategoryId { get; set; }

    public string? Name { get; set; }

    public int? DisplayOrder { get; set; }

    public string? SeoTitle { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<GenderCategory> GenderCategories { get; } = new List<GenderCategory>();

    public virtual ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();
}
