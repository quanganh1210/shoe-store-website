using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ThietKeWebBTL_User2.Models;

public partial class ProductDetail
{
    public long? Quantity { get; set; }

    public long ProductDetailId { get; set; }

    public string? Image { get; set; }

    public long? ProductId { get; set; }

    public long? ColorId { get; set; }

    public long? SizeId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public decimal? UnitSupplierPrice { get; set; }

    public decimal? UnitSellingPrice { get; set; }

    public double? Discount { get; set; }

    public virtual Color? Color { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<ImageProductDetail> ImageProductDetails { get; } = new List<ImageProductDetail>();
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual Product? Product { get; set; }

    public virtual Size? Size { get; set; }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    [DisplayName("Image")]
    public IFormFile? formFileImage { get; set; }
}
