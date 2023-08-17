using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class Role
{
    public long RoleId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
