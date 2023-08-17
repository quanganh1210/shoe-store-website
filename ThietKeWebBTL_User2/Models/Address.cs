using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class Address
{
    public long AddressId { get; set; }

    public string? Name { get; set; }

    public string? Note { get; set; }

    public string? Detail { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public virtual Account? EmailNavigation { get; set; }
}
