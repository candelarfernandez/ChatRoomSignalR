using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatRoom.Datos.Migrations
{
    /// <inheritdoc />
    public partial class AddImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "fotoProductoNombre",
                table: "Sala",
                type: "varbinary(1000)",
                unicode: false,
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldUnicode: false,
                oldMaxLength: 1000,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "fotoProductoNombre",
                table: "Sala",
                type: "varchar(1000)",
                unicode: false,
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(1000)",
                oldUnicode: false,
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
