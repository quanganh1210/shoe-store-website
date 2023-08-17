using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class OrderDetail
{
    public long? Quantity { get; set; }

    public decimal? UnitSellingPrice { get; set; }

    public long OrderId { get; set; }

    public long ProductDetailId { get; set; }

    public virtual TbOrder Order { get; set; } = null!;

    public virtual ProductDetail ProductDetail { get; set; } = null!;
}
