using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class ImageProductDetail
{
    public long ImageId { get; set; }

    public long ProductDetailId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Image Image { get; set; } = null!;

    public virtual ProductDetail ProductDetail { get; set; } = null!;
}
