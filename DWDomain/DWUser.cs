using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWDomain;
[Table("Users")]
public class DWUser {
    [Key]
    [Column("userId")]
    public uint UserId { get; set; }

    [StringLength(100)]
    [Column("firstName")]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    [Column("lastName")]
    public string LastName { get; set; } = null!;
}
