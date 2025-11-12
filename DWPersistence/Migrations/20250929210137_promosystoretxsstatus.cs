using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWPersistence.Migrations
{
    /// <inheritdoc />
    public partial class promosystoretxsstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promos",
                columns: table => new
                {
                    promoId = table.Column<long>(type: "bigint", nullable: false, comment: "Id de la promoción"),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Nombre de la promoción"),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Descripción de la promoción"),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0, comment: "Estado de la promoción"),
                    startDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Vigencia-inicio. Fecha y hora UTC (sumando el timzone hora local)"),
                    endDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha Hora del fin de la promoción"),
                    insDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha Hora registro"),
                    insUserId = table.Column<long>(type: "bigint", nullable: false, comment: "Id del usuario que registro la promoción")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promos", x => x.promoId);
                },
                comment: "Tabla de las promociones");

            migrationBuilder.CreateTable(
                name: "StoreTxsStatus",
                columns: table => new
                {
                    status = table.Column<short>(type: "smallint", nullable: false, comment: "Id"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Name"),
                    insUserId = table.Column<int>(type: "int", nullable: true, comment: "Usuario insercion"),
                    insDate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Fecha insercion"),
                    insDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha Hora insercion"),
                    insTimestamp = table.Column<long>(type: "bigint", nullable: true, comment: "Epoch insercion")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTxsStatus", x => x.status);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promos");

            migrationBuilder.DropTable(
                name: "StoreTxsStatus");
        }
    }
}
