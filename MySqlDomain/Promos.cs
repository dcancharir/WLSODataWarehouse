using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDomain;
[Table("Promos")]
public class Promos {
    /// <summary>
    /// Id de la promoción
    /// </summary>
    [Key]
    [Column("promoId",TypeName = "int(10) unsigned")]
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
    [Column("status",TypeName = "tinyint(3) unsigned")]
    public byte? Status { get; set; }

    /// <summary>
    /// Vigencia-inicio. Fecha y hora UTC (sumando el timzone hora local)
    /// </summary>
    [Column("startDateTime",TypeName = "datetime")]
    public DateTime? StartDateTime { get; set; }

    /// <summary>
    /// Fecha Hora del fin de la promoción
    /// </summary>
    [Column("endDatetime",TypeName = "datetime")]
    public DateTime? EndDatetime { get; set; }

    /// <summary>
    /// Fecha Hora registro
    /// </summary>
    [Column("insDatetime",TypeName = "datetime")]
    public DateTime? InsDatetime { get; set; }

    /// <summary>
    /// Id del usuario que registro la promoción
    /// </summary>
    [Column("insUserId",TypeName = "int(10) unsigned")]
    public uint InsUserId { get; set; }
}
