using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStateColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productState",
                table: "Products",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Products",
                newName: "productState");
        }
    }
}
