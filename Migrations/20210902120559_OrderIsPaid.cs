using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class OrderIsPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "18072b45-1b60-4c67-b957-f876b6e7b83b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "dae00591-ecad-4c34-ae9c-4078375d7726");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Orders",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5da683c0-f80b-4142-9dbe-6e377d72b19f", "b5e23e8f-2f04-4ae9-bdd2-f0db44562e18", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c131915-36a3-49cc-976b-e64e237e2dd9", "d09dd7be-b388-4ea0-bee0-3807c324fe65", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5da683c0-f80b-4142-9dbe-6e377d72b19f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6c131915-36a3-49cc-976b-e64e237e2dd9");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18072b45-1b60-4c67-b957-f876b6e7b83b", "3cf3a5ae-a8a6-476c-ae26-c34056bab8ad", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dae00591-ecad-4c34-ae9c-4078375d7726", "140705e4-215f-4e3f-8a7b-acfea0e3d89e", "Administrator", "ADMINISTRATOR" });
        }
    }
}
