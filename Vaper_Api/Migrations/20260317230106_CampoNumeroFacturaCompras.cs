using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class CampoNumeroFacturaCompras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Compras",
                newName: "FechaRegistro");

            migrationBuilder.AddColumn<string>(
                name: "NumeroFactura",
                table: "Compras",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroFactura",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "Compras",
                newName: "FechaCreacion");
        }
    }
}
