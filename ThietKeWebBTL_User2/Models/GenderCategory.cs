using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class GenderCategory
{
    public long GenderId { get; set; }

    public long CategoryId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;
}
