using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWDomain;
public class DWBonusStatusLog {
    /// <summary>
    /// Id de Log
    /// </summary>
    [Key]
    [Column("logId")]
    public ulong LogId { get; set; }

    /// <summary>
    /// Id de Bono
    /// </summary>
    [Column("bonusId")]
    public ulong BonusId { get; set; }

    /// <summary>
    /// Id de la promoción
    /// </summary>
    [Column("promoId")]
    public uint PromoId { get; set; }

    /// <summary>
    /// Status del bono. Valores: assigned, applied, expired, finished, canceled, etc
    /// </summary>
    [StringLength(10)]
    [Column("status")]
    public string Status { get; set; } = null!;

    /// <summary>
    /// Fecha actualizacion de status
    /// </summary>
    [Column("setDate")]
    public DateOnly? SetDate { get; set; }

    /// <summary>
    /// Fecha Hora actualizacion de status
    /// </summary>
    [Column("setDatetime")]
    public DateTime? SetDatetime { get; set; }
}
