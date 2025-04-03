using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.MySql;

[Index("AssociateId", Name = "idxAssociateId")]
[Index("GameId", Name = "idxGameId")]
[Index("InsTimestamp", Name = "idxInsTimestamp")]
[Index("PlayerId", Name = "idxPlayerId")]
[Index("ProviderId", Name = "idxProviderId")]
[Index("Status", Name = "idxStatus")]
[Index("StoreId", Name = "idxStoreId")]
[Index("Type", Name = "idxType")]
[MySqlCharSet("utf8mb3")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class RealGameEvent
{
    /// <summary>
    /// ID de la transaccion
    /// </summary>
    [Key]
    [Column("eventId")]
    public ulong EventId { get; set; }

    /// <summary>
    /// ID del store del player
    /// </summary>
    [Column("storeId")]
    [StringLength(30)]
    public string StoreId { get; set; } = null!;

    /// <summary>
    /// Id del player
    /// </summary>
    [Column("playerId")]
    public int PlayerId { get; set; }

    /// <summary>
    /// ID del proveedor, revisar tabla WLSOProvidersDB.Providers
    /// </summary>
    [Column("providerId")]
    public int ProviderId { get; set; }

    /// <summary>
    /// ID del juego
    /// </summary>
    [Column("gameId")]
    [StringLength(60)]
    public string? GameId { get; set; }

    /// <summary>
    /// DEBIT | CREDIT
    /// </summary>
    [Column("type")]
    [StringLength(16)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// Monto de la apuesta
    /// </summary>
    [Column("amount")]
    public long Amount { get; set; }

    /// <summary>
    /// Estado del evento. 0 -&gt; Iniciado, 1 -&gt; Procesado o completado (actualizo el balance del player), 5 -&gt; Denegado (Ej: se solicita un credit cuando la apuesta fue un bet_bonus)
    /// </summary>
    [Column("status")]
    public byte? Status { get; set; }

    /// <summary>
    /// Fecha Hora registro
    /// </summary>
    [Column("insDatetime", TypeName = "datetime")]
    public DateTime? InsDatetime { get; set; }

    /// <summary>
    /// Epoch registro
    /// </summary>
    [Column("insTimestamp")]
    public long? InsTimestamp { get; set; }

    /// <summary>
    /// &quot;REAL&quot;: Usado para juegos con su dinero | &quot;BONUS&quot;: Usado para juegos con bono  
    /// </summary>
    [Column("coinsType")]
    [StringLength(16)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? CoinsType { get; set; }

    /// <summary>
    /// ID del asociado
    /// </summary>
    [Column("associateId")]
    [StringLength(11)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? AssociateId { get; set; }
}
