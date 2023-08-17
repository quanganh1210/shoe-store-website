using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class ProductCategory
{
    public long CategoryId { get; set; }

    public long ProductId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Product? Product { get; set; }
}
