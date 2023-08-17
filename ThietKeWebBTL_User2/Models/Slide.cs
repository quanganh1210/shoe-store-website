using System;
using System.Collections.Generic;

namespace ThietKeWebBTL_User2.Models;

public partial class Slide
{
    public long SlideId { get; set; }

    public string? Image { get; set; }

    public string? Text1 { get; set; }

    public string? Text2 { get; set; }

    public int? DisplayOrder { get; set; }

    public string? Link { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }
}
