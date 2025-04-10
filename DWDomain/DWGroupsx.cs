using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DWDomain;
public class DWGroupsx {
    /// <summary>
    /// Id del Tag
    /// </summary>
    [Key]
    [Column("groupId")]
    public int GroupId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    /// <summary>
    /// 1 -&gt; Tag, 2 -&gt; Segment
    /// </summary>
    [Column("type")]
    [StringLength(255)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 1: Active, 2: Delete
    /// </summary>
    [Column("status")]
    public sbyte? Status { get; set; }
}
