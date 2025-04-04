using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DWDomain;
/// <summary>
/// Informacion de los Games
/// </summary>
[PrimaryKey("GameId", "ProviderId")]
public partial class DWGame {
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
