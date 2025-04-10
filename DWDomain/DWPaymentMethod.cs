using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWDomain;
[PrimaryKey("MethodId", "Type")]
[Table("PaymentMethod")]
public partial class DWPaymentMethod {

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
