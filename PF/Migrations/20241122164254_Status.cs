using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF.Migrations
{
    /// <inheritdoc />
    public partial class Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_StatusPedidos_StatusPedidoIdStatusPedido",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "PedidoStatusId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "StatusPedidoIdStatusPedido",
                table: "Pedidos",
                newName: "StatusPedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_StatusPedidoIdStatusPedido",
                table: "Pedidos",
                newName: "IX_Pedidos_StatusPedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_StatusPedidos_StatusPedidoId",
                table: "Pedidos",
                column: "StatusPedidoId",
                principalTable: "StatusPedidos",
                principalColumn: "IdStatusPedido",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_StatusPedidos_StatusPedidoId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "StatusPedidoId",
                table: "Pedidos",
                newName: "StatusPedidoIdStatusPedido");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_StatusPedidoId",
                table: "Pedidos",
                newName: "IX_Pedidos_StatusPedidoIdStatusPedido");

            migrationBuilder.AddColumn<int>(
                name: "PedidoStatusId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_StatusPedidos_StatusPedidoIdStatusPedido",
                table: "Pedidos",
                column: "StatusPedidoIdStatusPedido",
                principalTable: "StatusPedidos",
                principalColumn: "IdStatusPedido",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
