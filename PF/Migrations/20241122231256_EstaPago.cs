using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF.Migrations
{
    /// <inheritdoc />
    public partial class EstaPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaPago",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaPago",
                table: "Pedidos");
        }
    }
}
