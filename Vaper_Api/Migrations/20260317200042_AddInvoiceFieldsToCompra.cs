using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceFieldsToCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFactura",
                table: "Compras",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroFactura",
                table: "Compras",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFactura",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "NumeroFactura",
                table: "Compras");
        }
    }
}
