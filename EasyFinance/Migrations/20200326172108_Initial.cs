using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyFinance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MatchPattern = table.Column<string>(nullable: true),
                    GenericCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MatchPattern = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceiptId = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileBytes = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    PaymentMethodId = table.Column<int>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipts_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Продукти" },
                    { 12, "Інше" },
                    { 11, "Бензин" },
                    { 10, "Одяг" },
                    { 8, "Подарунок" },
                    { 7, "Книги/Журнали" },
                    { 9, "Дозвілля" },
                    { 5, "Таксі/Автобус" },
                    { 4, "Вечеря" },
                    { 3, "Обід" },
                    { 2, "Сніданок" },
                    { 6, "Потяг" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "GenericCode", "MatchPattern", "Name" },
                values: new object[,]
                {
                    { 1, "UAH", " ?грн.? ?", "Hryvnia" },
                    { 2, "EUR", null, "Euro" },
                    { 3, "USD", null, "US Dollar" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "MatchPattern", "Name" },
                values: new object[,]
                {
                    { 1, "г[ao]тівк[а]", "Готівка" },
                    { 2, "(к[ао]ртк[ао]|безг[ао]тівк[ао]в[ао])", "Картка" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CategoryId",
                table: "Receipts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CurrencyId",
                table: "Receipts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_PaymentMethodId",
                table: "Receipts",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptPhotos");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "PaymentMethods");
        }
    }
}
