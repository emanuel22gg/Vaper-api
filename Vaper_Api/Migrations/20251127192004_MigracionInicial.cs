using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Crear tabla Cotizaciones
            migrationBuilder.CreateTable(
                name: "Cotizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cotizaci__3214EC07", x => x.Id);
                });

            // Crear tabla DetalleCotizaciones
            migrationBuilder.CreateTable(
                name: "DetalleCotizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CotizacionId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___3214EC07", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Detalle_C__Cotiz",
                        column: x => x.CotizacionId,
                        principalTable: "Cotizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Crear índices
            migrationBuilder.CreateIndex(
                name: "IX_DetalleCotizaciones_CotizacionId",
                table: "DetalleCotizaciones",
                column: "CotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCotizaciones_ProductoId",
                table: "DetalleCotizaciones",
                column: "ProductoId");

            // Crear foreign key con Productos
            migrationBuilder.AddForeignKey(
                name: "FK__Detalle_C__Produ",
                table: "DetalleCotizaciones",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar tabla DetalleCotizaciones
            migrationBuilder.DropTable(
                name: "DetalleCotizaciones");

            // Eliminar tabla Cotizaciones
            migrationBuilder.DropTable(
                name: "Cotizaciones");
        }
    }
}