using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DWDomain;
/// <summary>
/// Informacion de Customer
/// </summary>
[Keyless]
public partial class DWCustomer {
    [Column("associateId")]
    [StringLength(30)]
    public string? AssociateId { get; set; }

    [Column("storeId")]
    [StringLength(30)]
    public string? StoreId { get; set; }

    [Column("playerId")]
    public uint PlayerId { get; set; }

    [Column("username")]
    [StringLength(250)]
    public string Username { get; set; } = null!;

    [Column("email")]
    [StringLength(250)]
    public string? Email { get; set; }

    [Column("firstName")]
    [StringLength(100)]
    public string? FirstName { get; set; }

    [Column("lastName")]
    [StringLength(100)]
    public string? LastName { get; set; }

    [Column("phone")]
    [StringLength(50)]
    public string? Phone { get; set; }

    [Column("active")]
    public sbyte? Active { get; set; }

    [Column("verified")]
    public sbyte? Verified { get; set; }

    [Column("excluded")]
    public sbyte? Excluded { get; set; }

    [Column("regDatetime", TypeName = "datetime")]
    public DateTime? RegDatetime { get; set; }

    [Column("birthdate")]
    public DateOnly? Birthdate { get; set; }

    [Column("addressDept")]
    [StringLength(150)]
    public string? AddressDept { get; set; }

    [Column("addressProv")]
    [StringLength(150)]
    public string? AddressProv { get; set; }

    [Column("addressDist")]
    [StringLength(150)]
    public string? AddressDist { get; set; }

    [Column("address", TypeName = "text")]
    public string? Address { get; set; }

    [Column("identId")]
    public Guid? IdentId { get; set; }

    [Column("identification")]
    [StringLength(50)]
    public string? Identification { get; set; }
}
