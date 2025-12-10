using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    /// <inheritdoc />
    public partial class RelacionFinalProductoImagenCorrecta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Imagenes__Produc__7F2BE32F",
                table: "Imagenes");

            // Evitar excepción si la columna ya no existe
            migrationBuilder.Sql(@"
IF COL_LENGTH('Productos','IdImagen') IS NOT NULL
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Productos]') AND [c].[name] = N'IdImagen');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Productos] DROP CONSTRAINT [' + @var0 + ']');
    ALTER TABLE [Productos] DROP COLUMN [IdImagen];
END
");

            migrationBuilder.AlterColumn<int>(
                name: "ProductoId",
                table: "Imagenes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Imagenes__Produc__7F2BE32F",
                table: "Imagenes",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Imagenes__Produc__7F2BE32F",
                table: "Imagenes");

            migrationBuilder.AddColumn<int>(
                name: "IdImagen",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductoId",
                table: "Imagenes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdImagen",
                table: "Productos",
                column: "IdImagen");

            migrationBuilder.AddForeignKey(
                name: "FK__Imagenes__Produc__7F2BE32F",
                table: "Imagenes",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Productos__IdIma__7E37BEF6",
                table: "Productos",
                column: "IdImagen",
                principalTable: "Imagenes",
                principalColumn: "IdImagen");
        }
    }
}