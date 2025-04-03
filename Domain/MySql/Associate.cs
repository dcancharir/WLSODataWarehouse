using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.MySql;

/// <summary>
/// Asociados
/// </summary>
[Index("Ruc", Name = "idxRuc")]
[Index("Status", Name = "idxStatus")]
public partial class Associate
{
    /// <summary>
    /// Ruc
    /// </summary>
    [Key]
    [Column("ruc")]
    [StringLength(11)]
    public string Ruc { get; set; } = null!;

    /// <summary>
    /// Nombre del asociado
    /// </summary>
    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Estado del asociado
    /// </summary>
    [Column("status")]
    public byte? Status { get; set; }
}
