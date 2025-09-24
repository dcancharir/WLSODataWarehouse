using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDomain;
[Table("Users")]
public class User {
    [Key]
    [Column("userId",TypeName = "int(10) unsigned")]
    public uint UserId { get; set; }

    [StringLength(100)]
    [Column("firstName")]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    [Column("lastName")]
    public string LastName { get; set; } = null!;
}
