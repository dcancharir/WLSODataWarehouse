using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWDomain;
[Table("Bonuses")]
public class DWBonuse {
    /// <summary>
    /// Id del bono
    /// </summary>
    [Key]
    [Column("bonusId")]
    public uint BonusId { get; set; }

    /// <summary>
    /// Id de la promoción
    /// </summary>
    [Column("promoId")]
    public uint PromoId { get; set; }

    /// <summary>
    /// Estado del bono
    /// </summary>
    [Column("status")]
    public byte? Status { get; set; }

    /// <summary>
    /// Id del player
    /// </summary>
    [Column("playerIdReal")]
    public uint? PlayerIdReal { get; set; }

    /// <summary>
    /// Id del player
    /// </summary>
    [Column("playerIdBonus")]
    public uint? PlayerIdBonus { get; set; }

    /// <summary>
    /// Monto del bono
    /// </summary>
    [Column("amount")]
    public long? Amount { get; set; }

    /// <summary>
    /// Monto ganado o balance conseguido con el bono antes de hacer la conversion.
    /// </summary>
    [Column("amountWin")]
    public long? AmountWin { get; set; }

    /// <summary>
    /// Monto de la transacción con la que se accedio al bono
    /// </summary>
    [Column("txAmount")]
    public int? TxAmount { get; set; }

    /// <summary>
    /// Monto pagado
    /// </summary>
    [Column("txPayOutAmount")]
    public int? TxPayOutAmount { get; set; }

    /// <summary>
    /// epoch de registro
    /// </summary>
    [Column("insTimestamp")]
    public long? InsTimestamp { get; set; }

    /// <summary>
    /// activaite epoch
    /// </summary>
    [Column("activatedTimestamp")]
    public long? ActivatedTimestamp { get; set; }

    /// <summary>
    /// expirate epoch
    /// </summary>
    [Column("endTimestamp")]
    public long? EndTimestamp { get; set; }
}
