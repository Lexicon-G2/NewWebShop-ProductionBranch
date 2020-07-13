using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppForWebshop.Data.Migrations
{
    public partial class newmigrationforuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "848e4f8e-93a5-4fae-8665-1852ff67cda6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "cb92a219-8b69-49c0-8a4b-81d962d8c8fd");
        }
    }
}
