using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class GenderProduct
{
    public long GenderId { get; set; }

    public long ProductId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual Product? Product { get; set; }
}
