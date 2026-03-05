using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlazoAbonosToVentaPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlazoAbonos",
                table: "Venta_Pedidos",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlazoAbonos",
                table: "Venta_Pedidos");
        }
    }
}
