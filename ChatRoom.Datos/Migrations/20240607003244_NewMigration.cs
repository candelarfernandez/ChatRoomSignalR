using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatRoom.Datos.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "username",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "username");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Users",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idProducto",
                table: "Sala",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sala_idProducto",
                table: "Sala",
                column: "idProducto",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Sala__idProducto__6FE99F9F",
                table: "Sala",
                column: "idProducto",
                principalTable: "Producto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Sala__idProducto__6FE99F9F",
                table: "Sala");

            migrationBuilder.DropIndex(
                name: "IX_Sala_idProducto",
                table: "Sala");

            migrationBuilder.DropColumn(
                name: "idProducto",
                table: "Sala");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Usuario",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
