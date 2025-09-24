using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySqlDomain;
/// <summary>
/// Tabla de los bonos asignados
/// </summary>
//[Index("playerIdReal", Name = "idxPlayerIdReal")]
//[Index("promoId", Name = "idxPromoId")]
//[Index("status", Name = "idxStatus")]
[Table("Bonuses")]
public class Bonuse {
    /// <summary>
    /// Id del bono
    /// </summary>
    [Key]
    [Column("bonusId",TypeName = "int(10) unsigned")]
    public uint BonusId { get; set; }

    /// <summary>
    /// Id de la promoción
    /// </summary>
    [Column("promoId", TypeName = "int(10) unsigned")]
    public uint PromoId { get; set; }

    /// <summary>
    /// Estado del bono
    /// </summary>
    [Column("status",TypeName = "tinyint(4)")]
    public sbyte? Status { get; set; }

    /// <summary>
    /// Id del player
    /// </summary>
    [Column("playerIdReal",TypeName = "int(10) unsigned")]
    public uint? PlayerIdReal { get; set; }

    /// <summary>
    /// Id del player
    /// </summary>
    [Column("playerIdBonus", TypeName = "int(10) unsigned")]
    public uint? PlayerIdBonus { get; set; }

    /// <summary>
    /// Monto del bono
    /// </summary>
    [Column("amount",TypeName = "bigint(20)")]
    public long? Amount { get; set; }

    /// <summary>
    /// Monto ganado o balance conseguido con el bono antes de hacer la conversion.
    /// </summary>
    [Column("amountWin", TypeName = "bigint(20)")]
    public long? AmountWin { get; set; }

    /// <summary>
    /// Monto de la transacción con la que se accedio al bono
    /// </summary>
    [Column("txAmount", TypeName = "int(11)")]
    public int? TxAmount { get; set; }

    /// <summary>
    /// Monto pagado
    /// </summary>
    [Column("txPayOutAmount", TypeName = "int(11)")]
    public int? TxPayOutAmount { get; set; }

    /// <summary>
    /// epoch de registro
    /// </summary>
    [Column("insTimestamp", TypeName = "bigint(20)")]
    public long? InsTimestamp { get; set; }

    /// <summary>
    /// activaite epoch
    /// </summary>
    [Column("activatedTimestamp", TypeName = "bigint(20)")]
    public long? ActivatedTimestamp { get; set; }

    /// <summary>
    /// expirate epoch
    /// </summary>
    [Column("endTimestamp",TypeName = "bigint(20)")]
    public long? EndTimestamp { get; set; }
}
