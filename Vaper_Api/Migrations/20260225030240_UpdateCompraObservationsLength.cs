using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompraObservationsLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "Compras",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            // Asegurar que el ID 3 corresponda a 'Anulada'
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM Estados WHERE Id = 3)
                BEGIN
                    UPDATE Estados SET NombreEstado = 'Anulada' WHERE Id = 3;
                END
                ELSE
                BEGIN
                    SET IDENTITY_INSERT Estados ON;
                    INSERT INTO Estados (Id, NombreEstado) VALUES (3, 'Anulada');
                    SET IDENTITY_INSERT Estados OFF;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "Compras",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
