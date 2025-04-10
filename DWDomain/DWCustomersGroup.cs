using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DWDomain;
public partial class DWCustomersGroup {
    [Key]
    [Column("playerId")]
    public long PlayerId { get; set; }

    [Key]
    [Column("groupId")]
    public int GroupId { get; set; }

    [Column("groupType")]
    public int? GroupType { get; set; }
}
