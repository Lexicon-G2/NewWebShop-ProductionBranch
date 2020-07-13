using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppForWebshop.Data.Migrations
{
    public partial class orderrefMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderReferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderIdId = table.Column<int>(nullable: false),
                    ProductIdId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderReferences_Orders_OrderIdId",
                        column: x => x.OrderIdId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderReferences_Products_ProductIdId",
                        column: x => x.ProductIdId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "382a7a57-62df-49ae-b7b5-436dd6f1446f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8e36ebd4-a23a-46db-8232-4a94aa7a3608");

            migrationBuilder.CreateIndex(
                name: "IX_OrderReferences_OrderIdId",
                table: "OrderReferences",
                column: "OrderIdId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderReferences_ProductIdId",
                table: "OrderReferences",
                column: "ProductIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderReferences");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "dd5d6f6c-9653-4d98-9e55-37408ab88a8c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "10f54a58-edd3-499d-9941-a59ea2e5d90f");
        }
    }
}
