using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class CambiarEstadoProveedoresABool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "Proveedores",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "Activo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Proveedores",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                defaultValue: "Activo",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}
