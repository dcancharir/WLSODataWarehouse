using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySqlDomain;

/// <summary>
/// Cuentas de Players de dinero real, estas se geeran cuando el cliente realiza un registro
/// </summary>
public partial class Player
{
    /// <summary>
    /// ID autoincremental para el player
    /// </summary>
    [Key]
    [Column("playerId")]
    public uint PlayerId { get; set; }

    /// <summary>
    /// Coins reales
    /// </summary>
    [Column("coins")]
    public ulong? Coins { get; set; }

    /// <summary>
    /// Balance retirable del usuario
    /// </summary>
    [Column("coinsPayOut")]
    public ulong? CoinsPayOut { get; set; }
}
