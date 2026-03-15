using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionEnDevoluciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Devoluciones");

            migrationBuilder.RenameColumn(
                name: "VentaPedidoId",
                table: "Detalle_Devoluciones",
                newName: "DetalleVentaPedidoId");

            // Para evitar el error #15248 de SQL Server con sp_rename en índices generados
            // Y error de 'does not exist'
            // migrationBuilder.DropIndex(
            //     name: "IX_Detalle_Devoluciones_VentaPedidoId",
            //     table: "Detalle_Devoluciones");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Devoluciones_DetalleVentaPedidoId",
                table: "Detalle_Devoluciones",
                column: "DetalleVentaPedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DetalleVentaPedidoId",
                table: "Detalle_Devoluciones",
                newName: "VentaPedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Detalle_Devoluciones_DetalleVentaPedidoId",
                table: "Detalle_Devoluciones",
                newName: "IX_Detalle_Devoluciones_VentaPedidoId");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Devoluciones",
                type: "bit",
                nullable: true);
        }
    }
}
