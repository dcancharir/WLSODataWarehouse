using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySqlDomain;

/// <summary>
/// Tabla de registros del balance los procesadores de pago
/// </summary>
[Index("State", Name = "idxState")]
public partial class Processor
{
    /// <summary>
    /// Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ...
    /// </summary>
    [Key]
    [Column("processorId")]
    [StringLength(15)]
    public string ProcessorId { get; set; } = null!;

    /// <summary>
    /// Nombre del provesador
    /// </summary>
    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Estado actual del processador 0 &lt;- DESACTIVADO | 1 &lt;- ACTIVADO
    /// </summary>
    [Column("state")]
    public sbyte? State { get; set; }
}
