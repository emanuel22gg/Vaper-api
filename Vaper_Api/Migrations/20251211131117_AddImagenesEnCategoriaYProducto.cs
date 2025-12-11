using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaper_Api.Migrations
{
    public partial class AddImagenesEnCategoriaYProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar solo la columna que NO existe en Productos
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Productos",
                type: "text",
                nullable: true);

            // *** NO agregar IdImagen en Productos porque YA EXISTE ***

            // Agregar IdImagen en Categoria_Productos
            migrationBuilder.AddColumn<int>(
                name: "IdImagen",
                table: "Categoria_Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Productos_IdImagen",
                table: "Categoria_Productos",
                column: "IdImagen");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Productos_Imagenes_IdImagen",
                table: "Categoria_Productos",
                column: "IdImagen",
                principalTable: "Imagenes",
                principalColumn: "IdImagen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Productos_Imagenes_IdImagen",
                table: "Categoria_Productos");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_Productos_IdImagen",
                table: "Categoria_Productos");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "IdImagen",
                table: "Categoria_Productos");

            // *** NO borrar IdImagen de Productos porque NO la creó esta migración ***
        }
    }
}
