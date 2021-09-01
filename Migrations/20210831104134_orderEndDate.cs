using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class orderEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "18b5a8ce-342e-466c-b05d-c729df58c8b8");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f7a14655-e874-46b3-839a-2cec561da1e9");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Orders",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18072b45-1b60-4c67-b957-f876b6e7b83b", "3cf3a5ae-a8a6-476c-ae26-c34056bab8ad", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dae00591-ecad-4c34-ae9c-4078375d7726", "140705e4-215f-4e3f-8a7b-acfea0e3d89e", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "18072b45-1b60-4c67-b957-f876b6e7b83b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "dae00591-ecad-4c34-ae9c-4078375d7726");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f7a14655-e874-46b3-839a-2cec561da1e9", "124e105c-3fa3-4ec1-9d5c-965e7734e71f", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18b5a8ce-342e-466c-b05d-c729df58c8b8", "4bbc7bbf-7974-41c5-991e-32dba5fe409d", "Administrator", "ADMINISTRATOR" });
        }
    }
}
