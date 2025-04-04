using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySqlDomain;

[Index("Active", Name = "idxActive")]
[Index("ProviderId", Name = "idxProviderId")]
public partial class Brand
{
    /// <summary>
    /// ID de la marca
    /// </summary>
    [Key]
    [Column("brandId")]
    [StringLength(15)]
    public string BrandId { get; set; } = null!;

    /// <summary>
    /// ID del provider
    /// </summary>
    [Column("providerId")]
    public uint ProviderId { get; set; }

    /// <summary>
    /// Nombre Brand
    /// </summary>
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 1 -&gt; Activo | 0 -&gt; Inactivo
    /// </summary>
    [Column("active")]
    public sbyte? Active { get; set; }
}
