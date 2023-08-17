using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class CreditCard
{
    public string? NameOnCard { get; set; }

    public string CardId { get; set; } = null!;

    public string? CardNumber { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Cvv { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public virtual Account? EmailNavigation { get; set; }
}
