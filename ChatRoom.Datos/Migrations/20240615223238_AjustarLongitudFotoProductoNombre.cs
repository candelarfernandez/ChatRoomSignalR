using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatRoom.Datos.Migrations
{
    /// <inheritdoc />
    public partial class AjustarLongitudFotoProductoNombre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Sala__idProducto__6FE99F9F",
                table: "Sala");

            migrationBuilder.DropForeignKey(
                name: "FK__Venta__IdProduct__4D94879B",
                table: "Venta");

            migrationBuilder.AlterColumn<int>(
                name: "IdProducto",
                table: "Venta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "fotoProductoNombre",
                table: "Sala",
                type: "varchar(1000)",
                unicode: false,
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sala_Producto",
                table: "Sala",
                column: "idProducto",
                principalTable: "Producto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Venta__IdProduct__4D94879B",
                table: "Venta",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sala_Producto",
                table: "Sala");

            migrationBuilder.DropForeignKey(
                name: "FK__Venta__IdProduct__4D94879B",
                table: "Venta");

            migrationBuilder.AlterColumn<int>(
                name: "IdProducto",
                table: "Venta",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "fotoProductoNombre",
                table: "Sala",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldUnicode: false,
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Sala__idProducto__6FE99F9F",
                table: "Sala",
                column: "idProducto",
                principalTable: "Producto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Venta__IdProduct__4D94879B",
                table: "Venta",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "id");
        }
    }
}
