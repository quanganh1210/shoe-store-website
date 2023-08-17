using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class CustomerMessage
{
    public long CustomerMessageId { get; set; }

    public string? ContactEmail { get; set; }

    public string? Content { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public virtual Account? EmailNavigation { get; set; }
}
