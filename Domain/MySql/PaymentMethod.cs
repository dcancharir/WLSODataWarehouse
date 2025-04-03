using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.MySql;

[PrimaryKey("MethodId", "Type")]
[Index("Active", Name = "active")]
public partial class PaymentMethod
{
    /// <summary>
    /// ID del metodo del pago. Ej: NIUBIZ_CARDS
    /// </summary>
    [Key]
    [Column("methodId")]
    [StringLength(25)]
    public string MethodId { get; set; } = null!;

    /// <summary>
    /// Nombre del metodo de pago
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    /// <summary>
    /// Tipo: PAYIN, PAYOUT
    /// </summary>
    [Key]
    [Column("type")]
    [StringLength(10)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 1 -&gt; True | 0 -&gt; False
    /// </summary>
    [Column("active")]
    public string? Active { get; set; }
}
