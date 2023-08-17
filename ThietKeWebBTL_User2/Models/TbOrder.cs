using System;
using System.ComponentModel.DataAnnotations;

namespace ThietKeWebBTL_User2.Models;

public partial class TbOrder
{
    public long OrderId { get; set; }

    
    public DateTime? OrderDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public decimal? DeliveryFee { get; set; }

    public string? Email { get; set; }

    public long? Id { get; set; }

    public long? PaymentId { get; set; }

    [Required]
    public string? Address { get; set; }

    public string? AddressNote { get; set; }

    public virtual Account? EmailNavigation { get; set; }

    public virtual DeliveryCompany? IdNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual Payment? Payment { get; set; }
}
