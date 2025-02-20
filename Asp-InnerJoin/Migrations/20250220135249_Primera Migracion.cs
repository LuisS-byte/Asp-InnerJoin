using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp_InnerJoin.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID_ROL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROL_NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID_ROL);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USU_NOMBRE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    USU_EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    USU_FECHA_REGISTRO = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ID_ROL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID_USUARIO);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_ID_ROL",
                        column: x => x.ID_ROL,
                        principalTable: "Roles",
                        principalColumn: "ID_ROL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ID_PRODUCTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PROD_NOMBRE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PROD_PRECIO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PROD_FECHA_CREACION = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ID_PRODUCTO);
                    table.ForeignKey(
                        name: "FK_Productos_Usuarios_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "Usuarios",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ID_USUARIO",
                table: "Productos",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ID_ROL",
                table: "Usuarios",
                column: "ID_ROL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
