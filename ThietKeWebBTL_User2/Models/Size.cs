using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThietKeWebBTL_User2.Models;

public partial class Size
{
    public long SizeId { get; set; }

    public string? Name { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<ProductDetail> ProductDetails { get; } = new List<ProductDetail>();
}
