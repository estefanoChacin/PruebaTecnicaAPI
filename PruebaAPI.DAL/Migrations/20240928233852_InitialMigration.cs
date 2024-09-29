using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.IdCategoria);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datedCreate = table.Column<DateTime>(type: "datetime(6)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    IdProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Stock = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    DatedCreate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    CategoriaIdCategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.IdProduct);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoriaIdCategoria",
                        column: x => x.CategoriaIdCategoria,
                        principalTable: "Category",
                        principalColumn: "IdCategoria");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_User_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "IdCategoria", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Autos" },
                    { 2, null, "Motocicletas" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "IdProduct", "CategoriaIdCategoria", "DatedCreate", "Description", "IdCategoria", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 9, 28, 18, 38, 51, 790, DateTimeKind.Local).AddTicks(3239), "De la linea deportiva", 2, "MT-15", 16000000m, 2 },
                    { 2, null, new DateTime(2024, 9, 28, 18, 38, 51, 790, DateTimeKind.Local).AddTicks(3245), "un auto de excelente gama", 1, "Mazda 3", 25000000m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "IdRol", "datedCreate", "name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EMPLEADO" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "IdUser", "CreatedDate", "Email", "IdRol", "IsActive", "Name", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 28, 18, 38, 51, 790, DateTimeKind.Local).AddTicks(3191), "alejrando@gmail.com", 1, (byte)1, "alejandro", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5" },
                    { 2, new DateTime(2024, 9, 28, 18, 38, 51, 790, DateTimeKind.Local).AddTicks(3205), "miguel@gmail.com", 2, (byte)1, "miguel", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoriaIdCategoria",
                table: "Product",
                column: "CategoriaIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdRol",
                table: "User",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
