using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWDomain;
[Table("BonusesStatus")]
public class DWBonusesStatus {
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Column("status")]
    public short Status { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    [StringLength(50)]
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    [StringLength(150)]
    [Column("desc")]
    public string? Desc { get; set; }

    /// <summary>
    /// Usuario insercion
    /// </summary>
    [Column("insUserId")]
    public int? InsUserId { get; set; }

    /// <summary>
    /// Fecha insercion
    /// </summary>
    [Column("insDate")]
    public DateOnly? InsDate { get; set; }

    /// <summary>
    /// Fecha Hora insercion
    /// </summary>
    [Column("insDatetime")]
    public DateTime? InsDatetime { get; set; }

    /// <summary>
    /// Epoch insercion
    /// </summary>
    [Column("insTimestamp")]
    public long? InsTimestamp { get; set; }
}
