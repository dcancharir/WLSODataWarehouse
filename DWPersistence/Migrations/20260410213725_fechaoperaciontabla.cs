using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWPersistence.Migrations
{
    /// <inheritdoc />
    public partial class fechaoperaciontabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "coinsType",
                table: "Players",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "playerIdRef",
                table: "Players",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "categoryId",
                table: "Games",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "insDatetime",
                table: "Games",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "emailChecked",
                table: "Customers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "gender",
                table: "Customers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "icCode",
                table: "Customers",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastLoginDatetime",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "phoneChecked",
                table: "Customers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "regDate",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "regTimestamp",
                table: "Customers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updDatetime",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DWHistorialMigracionWSLOs",
                columns: table => new
                {
                    fechaoperacion = table.Column<DateTime>(type: "date", nullable: false),
                    bonusstatuslog = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    customers = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    realgameevents = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DWHistorialMigracionWSLOs", x => x.fechaoperacion);
                },
                comment: "Historial de migracion de tablas con fechas conocidas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DWHistorialMigracionWSLOs");

            migrationBuilder.DropColumn(
                name: "coinsType",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "playerIdRef",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "insDatetime",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "emailChecked",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "icCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "lastLoginDatetime",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "phoneChecked",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "regDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "regTimestamp",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "updDatetime",
                table: "Customers");
        }
    }
}
