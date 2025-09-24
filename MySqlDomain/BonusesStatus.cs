using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDomain;
[Table("BonusesStatus")]
public partial class BonusesStatus {
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Column("status",TypeName = "smallint(6)")]
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
    [Column("insUserId",TypeName = "int(11)")]
    public int? InsUserId { get; set; }

    /// <summary>
    /// Fecha insercion
    /// </summary>
    [Column("insDate")]
    public DateOnly? InsDate { get; set; }

    /// <summary>
    /// Fecha Hora insercion
    /// </summary>
    [Column("insDatetime",TypeName = "datetime")]
    public DateTime? InsDatetime { get; set; }

    /// <summary>
    /// Epoch insercion
    /// </summary>
    [Column("insTimestamp",TypeName = "bigint(20)")]
    public long? InsTimestamp { get; set; }
}
