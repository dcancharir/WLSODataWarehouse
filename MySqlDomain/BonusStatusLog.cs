using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.EntityFrameworkCore.DataAnnotations;
using MySql.EntityFrameworkCore.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace MySqlDomain;
/// <summary>
/// Log de cambio de Status de los Bonos
/// </summary>
[Table("BonusStatusLog")]
//[Index("bonusId", Name = "idxBonusId")]
//[Index("bonusId", "status", Name = "idxBonusStatus", IsUnique = true)]
//[Index("setDate", Name = "idxSetDate")]
//[Index("status", Name = "idxStatus")]
[MySQLCharset("utf8")]
[MySqlCollation("utf8_general_ci")]
public class BonusStatusLog {
    /// <summary>
    /// Id de Log
    /// </summary>
    [Key]
    [Column("logId",TypeName = "bigint(20) unsigned")]
    public ulong LogId { get; set; }

    /// <summary>
    /// Id de Bono
    /// </summary>
    [Column("bonusId", TypeName = "bigint(20) unsigned")]
    public ulong BonusId { get; set; }

    /// <summary>
    /// Id de la promoción
    /// </summary>
    [Column("promoId",TypeName = "int(10) unsigned")]
    public uint PromoId { get; set; }

    /// <summary>
    /// Status del bono. Valores: assigned, applied, expired, finished, canceled, etc
    /// </summary>
    [StringLength(10)]
    [MySQLCharset("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
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
    [Column("setDatetime",TypeName = "datetime")]
    public DateTime? SetDatetime { get; set; }
}
