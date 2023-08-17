using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThietKeWebBTL_User2.Models;

public partial class Product
{
    public long ProductId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? SeoTitle { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<GenderProduct> GenderProducts { get; } = new List<GenderProduct>();
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<ProductDetail> ProductDetails { get; } = new List<ProductDetail>();
}
