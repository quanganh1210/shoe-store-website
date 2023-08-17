using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class Payment
{
    public long PaymentId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<TbOrder> TbOrders { get; } = new List<TbOrder>();
}
