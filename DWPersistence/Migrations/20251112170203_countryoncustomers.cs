using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWPersistence.Migrations
{
    /// <inheritdoc />
    public partial class countryoncustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "countryId",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "countryId",
                table: "Customers");
        }
    }
}
