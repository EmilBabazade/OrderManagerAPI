using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class VaqonNullableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "01702046-c1f6-4355-bfdf-e2b4979ea23b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fdb293e2-1e98-413e-8510-6c7d102f2204");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Vaqons",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DocType",
                table: "Vaqons",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e551d452-3cc6-444e-b29a-05ddf8a13815", "5e0d3d8a-8446-4ef9-8a3f-0f2aed53c018", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af959b83-dac3-4fed-ac22-0a6517ecd597", "11ff76e2-cce9-46f0-8ca7-ac4ebcd8e439", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "af959b83-dac3-4fed-ac22-0a6517ecd597");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e551d452-3cc6-444e-b29a-05ddf8a13815");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Vaqons",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocType",
                table: "Vaqons",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01702046-c1f6-4355-bfdf-e2b4979ea23b", "6be2e8db-5531-40f3-9d6e-2883933e3f6e", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fdb293e2-1e98-413e-8510-6c7d102f2204", "9dd3afb8-68d9-4ede-a798-c4caa774f208", "Administrator", "ADMINISTRATOR" });
        }
    }
}
