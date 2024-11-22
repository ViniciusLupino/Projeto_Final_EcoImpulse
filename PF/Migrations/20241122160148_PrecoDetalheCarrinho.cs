using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF.Migrations
{
    /// <inheritdoc />
    public partial class PrecoDetalheCarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PrecoUnitario",
                table: "DetalheCarrinhos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoUnitario",
                table: "DetalheCarrinhos");
        }
    }
}
