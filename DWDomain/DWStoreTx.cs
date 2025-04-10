using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DWDomain;
[Table("StoreTx")]
public partial class DWStoreTx {
    /// <summary>
    /// ID de la transaccion
    /// </summary>
    [Key]
    [Column("txId")]
    public uint TxId { get; set; }

    /// <summary>
    /// Id del player
    /// </summary>
    [Column("playerId")]
    public int PlayerId { get; set; }

    /// <summary>
    /// DEBIT | CREDIT | DEBIT_RESELLER | CREDIT_RESELLER
    /// </summary>
    [Column("type")]
    [StringLength(15)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// BONO_FINALIZE
    /// </summary>
    [Column("subType")]
    [StringLength(20)]
    public string SubType { get; set; } = null!;

    /// <summary>
    /// 0: init | 1: success | 2: error | 3: rejected | 4: pending | 5: expired
    /// </summary>
    [Column("status")]
    public int Status { get; set; }

    /// <summary>
    /// Metodo de pago
    /// </summary>
    [Column("paymentMethodId")]
    [StringLength(25)]
    public string PaymentMethodId { get; set; } = null!;

    /// <summary>
    /// Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ...
    /// </summary>
    [Column("processorId")]
    [StringLength(10)]
    public string? ProcessorId { get; set; }

    /// <summary>
    /// monto
    /// </summary>
    [Column("amount")]
    public int? Amount { get; set; }

    /// <summary>
    /// Coins reales antes de la transacction
    /// </summary>
    [Column("coinsBefore")]
    public long? CoinsBefore { get; set; }

    /// <summary>
    /// Coins reales despues de la transacction
    /// </summary>
    [Column("coins")]
    public long? Coins { get; set; }

    /// <summary>
    /// Id de la tienda
    /// </summary>
    [Column("userStoreId")]
    [StringLength(30)]
    public string? UserStoreId { get; set; }

    [Column("playerStoreId")]
    [StringLength(30)]
    public string? PlayerStoreId { get; set; }

    /// <summary>
    /// Id del usuario que realizo la operacion
    /// </summary>
    [Column("insUserId")]
    public int? InsUserId { get; set; }

    /// <summary>
    /// Fecha y hora de finalizacion de compra
    /// </summary>
    [Column("endDatetime", TypeName = "datetime")]
    public DateTime? EndDatetime { get; set; }

    /// <summary>
    /// Epoch al finalizar la transaccion
    /// </summary>
    [Column("endTimestamp")]
    public long? EndTimestamp { get; set; }

    [Column("associateId")]
    [StringLength(11)]
    public string? AssociateId { get; set; }
}
