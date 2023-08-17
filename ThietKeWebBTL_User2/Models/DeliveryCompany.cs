using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class DeliveryCompany
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<TbOrder> TbOrders { get; } = new List<TbOrder>();
}
