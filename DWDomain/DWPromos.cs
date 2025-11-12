using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWDomain;
[Table("Promos")]
public class DWPromos {
    /// <summary>
    /// Id de la promoción
    /// </summary>
    [Key]
    [Column("promoId")]
    public uint PromoId { get; set; }

    /// <summary>
    /// Nombre de la promoción
    /// </summary>
    [StringLength(500)]
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Descripción de la promoción
    /// </summary>
    [StringLength(500)]
    [Column("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Estado de la promoción
    /// </summary>
    [Column("status")]
    public byte? Status { get; set; }

    /// <summary>
    /// Vigencia-inicio. Fecha y hora UTC (sumando el timzone hora local)
    /// </summary>
    [Column("startDateTime")]
    public DateTime? StartDateTime { get; set; }

    /// <summary>
    /// Fecha Hora del fin de la promoción
    /// </summary>
    [Column("endDatetime")]
    public DateTime? EndDatetime { get; set; }

    /// <summary>
    /// Fecha Hora registro
    /// </summary>
    [Column("insDatetime")]
    public DateTime? InsDatetime { get; set; }

    /// <summary>
    /// Id del usuario que registro la promoción
    /// </summary>
    [Column("insUserId")]
    public uint InsUserId { get; set; }
}
