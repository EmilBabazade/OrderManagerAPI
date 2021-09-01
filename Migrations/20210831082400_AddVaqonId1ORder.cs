using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class AddVaqonId1ORder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "af959b83-dac3-4fed-ac22-0a6517ecd597");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e551d452-3cc6-444e-b29a-05ddf8a13815");

            migrationBuilder.AddColumn<int>(
                name: "VaqonId1",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7818df44-10dc-45cf-af44-010094f40d9e", "3bd7b2d1-0757-48f9-9497-3f77a209cba0", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2a3c607-d2bf-4b95-ae14-696748be2a52", "d4ce6808-7b88-45aa-a0a8-c2a63474cb91", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Vaqons_VaqonId2",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_VaqonId2",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7818df44-10dc-45cf-af44-010094f40d9e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a2a3c607-d2bf-4b95-ae14-696748be2a52");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Vaqons");

            migrationBuilder.DropColumn(
                name: "VaqonId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VaqonId2",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e551d452-3cc6-444e-b29a-05ddf8a13815", "5e0d3d8a-8446-4ef9-8a3f-0f2aed53c018", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af959b83-dac3-4fed-ac22-0a6517ecd597", "11ff76e2-cce9-46f0-8ca7-ac4ebcd8e439", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VaqonId",
                table: "Orders",
                column: "VaqonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Vaqons_VaqonId",
                table: "Orders",
                column: "VaqonId",
                principalTable: "Vaqons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
