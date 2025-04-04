using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySqlDomain;

[PrimaryKey("PlayerId", "GroupId")]
public partial class CustomersGroup
{
    [Key]
    [Column("playerId")]
    public long PlayerId { get; set; }

    [Key]
    [Column("groupId")]
    public int GroupId { get; set; }

    [Column("groupType")]
    public int? GroupType { get; set; }
}
