using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ThietKeWebBTL_User2.Models;

public partial class Image
{
    public long ImageId { get; set; }

    public string? FileName { get; set; }

    public int? DisplayOrder { get; set; }

    public virtual ICollection<ImageProductDetail> ImageProductDetails { get; } = new List<ImageProductDetail>();

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    [DisplayName("Image")]
    public IFormFile? formFileImage { get; set; }
}
