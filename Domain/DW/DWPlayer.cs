using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DW;
/// <summary>
/// Cuentas de Players de dinero real, estas se geeran cuando el cliente realiza un registro
/// </summary>
public partial class DWPlayer {
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
