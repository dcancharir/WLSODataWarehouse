using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DWDomain;

/// <summary>
/// Tiendas
/// </summary>
public partial class DWStore {
    [Key]
    [Column("storeId")]
    [StringLength(50)]
    public string StoreId { get; set; } = null!;

    [Column("associateId")]
    [StringLength(11)]
    public string? AssociateId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("status")]
    public byte? Status { get; set; }
}
