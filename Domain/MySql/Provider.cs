using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.MySql;

/// <summary>
/// Informacion de Proveedores
/// </summary>
[Index("Active", Name = "idxActive")]
[Index("Name", Name = "idxNameOrder")]
public partial class Provider
{
    /// <summary>
    /// ID autoincremental del provider
    /// </summary>
    [Key]
    [Column("providerId")]
    public uint ProviderId { get; set; }

    /// <summary>
    /// El nombre del provider
    /// </summary>
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 1 -&gt; Activo | 0 -&gt; Inactivo
    /// </summary>
    [Column("active")]
    public sbyte? Active { get; set; }
}
