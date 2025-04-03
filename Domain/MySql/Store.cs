using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.MySql;

/// <summary>
/// Tiendas
/// </summary>
[Index("AssociateId", Name = "idxAssociateId")]
[Index("Status", Name = "idxStatus")]
public partial class Store
{
    [Key]
    [Column("storeId")]
    [StringLength(50)]
    public string StoreId { get; set; } = null!;

    [Column("associateId")]
    [StringLength(11)]
    public string? AssociateId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("status")]
    public byte? Status { get; set; }
}
