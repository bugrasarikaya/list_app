using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace list_api.Migrations
{
    /// <inheritdoc />
    public partial class migration_03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Lists",
                newName: "TotalCost");

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "ListProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ListProducts");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "Lists",
                newName: "Cost");
        }
    }
}
