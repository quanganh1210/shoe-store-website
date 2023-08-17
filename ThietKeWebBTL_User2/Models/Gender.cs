using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class Gender
{
    public long GenderId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual ICollection<GenderCategory> GenderCategories { get; } = new List<GenderCategory>();

    public virtual ICollection<GenderProduct> GenderProducts { get; } = new List<GenderProduct>();
}
