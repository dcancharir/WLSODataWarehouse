using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWPersistence.Migrations
{
    /// <inheritdoc />
    public partial class tablasbonus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "insTimestamp",
                table: "StoreTxs",
                type: "bigint",
                nullable: true,
                comment: "Epoch registro");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Games",
                type: "nchar(60)",
                fixedLength: true,
                maxLength: 60,
                nullable: true,
                comment: "Tipo de juego: SLOTS, SPORTS, CASINO, POKER, etc...");

            migrationBuilder.AddColumn<string>(
                name: "ip",
                table: "Customers",
                type: "nvarchar(39)",
                maxLength: 39,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "lastLoginTimestamp",
                table: "Customers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                {
                    bonusId = table.Column<long>(type: "bigint", nullable: false, comment: "Id del bono"),
                    promoId = table.Column<long>(type: "bigint", nullable: false, comment: "Id de la promoción"),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0, comment: "Estado del bono"),
                    playerIdReal = table.Column<long>(type: "bigint", nullable: true, comment: "Id del player"),
                    playerIdBonus = table.Column<long>(type: "bigint", nullable: true, comment: "Id del player"),
                    amount = table.Column<long>(type: "bigint", nullable: true, defaultValue: 0L, comment: "Monto del bono"),
                    amountWin = table.Column<long>(type: "bigint", nullable: true, comment: "Monto ganado o balance conseguido con el bono antes de hacer la conversion."),
                    txAmount = table.Column<int>(type: "int", nullable: true, comment: "Monto de la transacción con la que se accedio al bono"),
                    txPayOutAmount = table.Column<int>(type: "int", nullable: true, comment: "Monto pagado"),
                    insTimestamp = table.Column<long>(type: "bigint", nullable: true, comment: "epoch de registro"),
                    activatedTimestamp = table.Column<long>(type: "bigint", nullable: true, comment: "activaite epoch"),
                    endTimestamp = table.Column<long>(type: "bigint", nullable: true, comment: "expirate epoch")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuses", x => x.bonusId);
                },
                comment: "Tabla de los bonos asignados");

            migrationBuilder.CreateTable(
                name: "BonusesStatus",
                columns: table => new
                {
                    status = table.Column<short>(type: "smallint", nullable: false, comment: "Id"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Name"),
                    desc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "Name"),
                    insUserId = table.Column<int>(type: "int", nullable: true, comment: "Usuario insercion"),
                    insDate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Fecha insercion"),
                    insDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha Hora insercion"),
                    insTimestamp = table.Column<long>(type: "bigint", nullable: true, comment: "Epoch insercion")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusesStatus", x => x.status);
                });

            migrationBuilder.CreateTable(
                name: "BonusStatusLog",
                columns: table => new
                {
                    logId = table.Column<decimal>(type: "decimal(20,0)", nullable: false, comment: "Id de Log"),
                    bonusId = table.Column<decimal>(type: "decimal(20,0)", nullable: false, comment: "Id de Bono"),
                    promoId = table.Column<long>(type: "bigint", nullable: false, comment: "Id de la promoción"),
                    status = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false, comment: "Status del bono. Valores: assigned, applied, expired, finished, canceled, etc"),
                    setDate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Fecha actualizacion de status"),
                    setDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha Hora actualizacion de status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusStatusLog", x => x.logId);
                },
                comment: "Log de cambio de Status de los Bonos");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<long>(type: "bigint", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropTable(
                name: "BonusesStatus");

            migrationBuilder.DropTable(
                name: "BonusStatusLog");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "insTimestamp",
                table: "StoreTxs");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ip",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "lastLoginTimestamp",
                table: "Customers");
        }
    }
}
