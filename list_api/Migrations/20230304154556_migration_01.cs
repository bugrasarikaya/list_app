using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace list_api.Migrations
{
    /// <inheritdoc />
    public partial class migration_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDBrand = table.Column<int>(type: "int", nullable: false),
                    IDCategory = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Brands_IDBrand",
                        column: x => x.IDBrand,
                        principalTable: "Brands",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_Categories_IDCategory",
                        column: x => x.IDCategory,
                        principalTable: "Categories",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDRole = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RefreshTokenExpireDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_IDRole",
                        column: x => x.IDRole,
                        principalTable: "Roles",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCategory = table.Column<int>(type: "int", nullable: false),
                    IDStatus = table.Column<int>(type: "int", nullable: false),
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    DateTimeCompleting = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTimeUpdating = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTimeCreating = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lists_Categories_IDCategory",
                        column: x => x.IDCategory,
                        principalTable: "Categories",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lists_Statuses_IDStatus",
                        column: x => x.IDStatus,
                        principalTable: "Statuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lists_Users_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ListProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDList = table.Column<int>(type: "int", nullable: false),
                    IDProduct = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListProducts_Lists_IDList",
                        column: x => x.IDList,
                        principalTable: "Lists",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ListProducts_Products_IDProduct",
                        column: x => x.IDProduct,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListProducts_IDList",
                table: "ListProducts",
                column: "IDList");

            migrationBuilder.CreateIndex(
                name: "IX_ListProducts_IDProduct",
                table: "ListProducts",
                column: "IDProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_IDCategory",
                table: "Lists",
                column: "IDCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_IDStatus",
                table: "Lists",
                column: "IDStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_IDUser",
                table: "Lists",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IDBrand",
                table: "Products",
                column: "IDBrand");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IDCategory",
                table: "Products",
                column: "IDCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IDRole",
                table: "Users",
                column: "IDRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListProducts");

            migrationBuilder.DropTable(
                name: "Lists");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
