using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ThietKeWebBTL_User2.Models;

public partial class Account
{
    [Required]
    public string? Password { get; set; }

    public string? Name { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public long? RoleId { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Image { get; set; }

    public long? GenderId { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();

    public virtual ICollection<CreditCard> CreditCards { get; } = new List<CreditCard>();

    public virtual ICollection<CustomerMessage> CustomerMessages { get; } = new List<CustomerMessage>();

    public virtual Gender? Gender { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<TbOrder> TbOrders { get; } = new List<TbOrder>();

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    [DisplayName("Image")]
    public IFormFile? formFileImage { get; set; }
}
