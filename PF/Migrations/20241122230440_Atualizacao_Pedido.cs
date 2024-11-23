using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao_Pedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailRemetente",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoRemetente",
                table: "Pedidos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetodoDePagamento",
                table: "Pedidos",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeRemetente",
                table: "Pedidos",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelefoneRemetente",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailRemetente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "EnderecoRemetente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "MetodoDePagamento",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "NomeRemetente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "TelefoneRemetente",
                table: "Pedidos");
        }
    }
}
