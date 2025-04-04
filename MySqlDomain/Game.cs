using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySqlDomain;

/// <summary>
/// Informacion de los Games
/// </summary>
[PrimaryKey("GameId", "ProviderId")]
[Index("Active", Name = "idxActive")]
[Index("BrandId", Name = "idxBrandId")]
[Index("NameBack", Name = "idxNameBack")]
[Index("ProviderId", Name = "idxProviderId")]
[Index("GameId", "ProviderId", Name = "idxUniqGame", IsUnique = true)]
public partial class Game
{
    /// <summary>
    /// ID del servicio a traves del que se enviaran mensajes
    /// </summary>
    [Key]
    [Column("gameId")]
    [StringLength(50)]
    public string GameId { get; set; } = null!;

    /// <summary>
    /// ID del provider
    /// </summary>
    [Key]
    [Column("providerId")]
    public uint ProviderId { get; set; }

    /// <summary>
    /// ID de brand
    /// </summary>
    [Column("brandId")]
    [StringLength(15)]
    public string BrandId { get; set; } = null!;

    /// <summary>
    /// Descripcion del game
    /// </summary>
    [Column("nameBack")]
    [StringLength(101)]
    public string NameBack { get; set; } = null!;

    /// <summary>
    /// 1 -&gt; Activo | 0 -&gt; Inactivo
    /// </summary>
    [Column("active")]
    public sbyte? Active { get; set; }
}
