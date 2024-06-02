using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatRoom.Datos.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    precioFinal = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__3213E83FE0211E48", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    username = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    dineroDisponible = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuario_Users_id",
                        column: x => x.id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    fotoProductoNombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idVendedor = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    usuariosConectados = table.Column<string>(type: "text", nullable: true),
                    activa = table.Column<bool>(type: "bit", nullable: true),
                    tiempoRestante = table.Column<long>(type: "bigint", nullable: true),
                    horaFinalizacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    horaUltimaOferta = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sala__3213E83F37C04928", x => x.id);
                    table.ForeignKey(
                        name: "FK__Sala__idVendedor__52593CB8",
                        column: x => x.idVendedor,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    idVendedor = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    idComprador = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venta__3213E83F603E29AC", x => x.id);
                    table.ForeignKey(
                        name: "FK__Venta__IdProduct__4D94879B",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Venta__idComprad__4F7CD00D",
                        column: x => x.idComprador,
                        principalTable: "Usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Venta__idVendedo__4E88ABD4",
                        column: x => x.idVendedor,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Oferta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    monto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    idComprador = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    idSala = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Oferta__3213E83F1D1D8CFF", x => x.id);
                    table.ForeignKey(
                        name: "FK__Oferta__idCompra__5535A963",
                        column: x => x.idComprador,
                        principalTable: "Usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Oferta__idSala__5629CD9C",
                        column: x => x.idSala,
                        principalTable: "Sala",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mensaje = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    idVenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__3213E83FF347DE52", x => x.id);
                    table.ForeignKey(
                        name: "FK__Notificac__idVen__59063A47",
                        column: x => x.idVenta,
                        principalTable: "Venta",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_idVenta",
                table: "Notificacion",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Oferta_idComprador",
                table: "Oferta",
                column: "idComprador");

            migrationBuilder.CreateIndex(
                name: "IX_Oferta_idSala",
                table: "Oferta",
                column: "idSala");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_idVendedor",
                table: "Sala",
                column: "idVendedor");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_idComprador",
                table: "Venta",
                column: "idComprador");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdProducto",
                table: "Venta",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_idVendedor",
                table: "Venta",
                column: "idVendedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "Oferta");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
