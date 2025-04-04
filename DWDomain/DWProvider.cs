using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DWDomain;
/// <summary>
/// Informacion de Proveedores
/// </summary>
public partial class DWProvider {
    /// <summary>
    /// ID autoincremental del provider
    /// </summary>
    [Key]
    [Column("providerId")]
    public uint ProviderId { get; set; }

    /// <summary>
    /// El nombre del provider
    /// </summary>
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 1 -&gt; Activo | 0 -&gt; Inactivo
    /// </summary>
    [Column("active")]
    public sbyte? Active { get; set; }
}
