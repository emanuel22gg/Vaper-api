using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class CamposEnCotizacionesYDevoluciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Detalle_D__Usuar__22751F6C",
                table: "Detalle_Devoluciones");

            /*
            migrationBuilder.DropIndex(
                name: "IX_Detalle_Devoluciones_UsuarioId",
                table: "Detalle_Devoluciones");
            */

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Detalle_Devoluciones");

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Devoluciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoTotal",
                table: "Devoluciones",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VentaPedidoId",
                table: "Devoluciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "DetalleCotizaciones",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Descuento",
                table: "Cotizaciones",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Cotizaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Cotizaciones",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Vigencia",
                table: "Cotizaciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_EstadoId",
                table: "Devoluciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_VentaPedidoId",
                table: "Devoluciones",
                column: "VentaPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_EstadoId",
                table: "Cotizaciones",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizaciones_Estados",
                table: "Cotizaciones",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_Estados",
                table: "Devoluciones",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_Venta_Pedidos",
                table: "Devoluciones",
                column: "VentaPedidoId",
                principalTable: "Venta_Pedidos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cotizaciones_Estados",
                table: "Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_Estados",
                table: "Devoluciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_Venta_Pedidos",
                table: "Devoluciones");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_EstadoId",
                table: "Devoluciones");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_VentaPedidoId",
                table: "Devoluciones");

            migrationBuilder.DropIndex(
                name: "IX_Cotizaciones_EstadoId",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Devoluciones");

            migrationBuilder.DropColumn(
                name: "MontoTotal",
                table: "Devoluciones");

            migrationBuilder.DropColumn(
                name: "VentaPedidoId",
                table: "Devoluciones");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "DetalleCotizaciones");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Vigencia",
                table: "Cotizaciones");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Detalle_Devoluciones",
                type: "int",
                nullable: true);

            /*
            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Devoluciones_UsuarioId",
                table: "Detalle_Devoluciones",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK__Detalle_D__Usuar__22751F6C",
                table: "Detalle_Devoluciones",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
            */
        }
    }
}
