using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Domain.DW;
/// <summary>
/// Asociados
/// </summary>
public partial class DWAssociate {
    /// <summary>
    /// Ruc
    /// </summary>
    [Key]
    [Column("ruc")]
    [StringLength(11)]
    public string Ruc { get; set; } = null!;

    /// <summary>
    /// Nombre del asociado
    /// </summary>
    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Estado del asociado
    /// </summary>
    [Column("status")]
    public byte? Status { get; set; }
}
