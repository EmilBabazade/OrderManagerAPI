using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class RemoveVaqonId1ORder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7818df44-10dc-45cf-af44-010094f40d9e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a2a3c607-d2bf-4b95-ae14-696748be2a52");

            migrationBuilder.AlterColumn<int>(
                name: "VaqonId1",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e5d0330-ca6e-4a0f-8300-45b71243902a", "95c4ac12-2045-4bda-b9ba-8dc754a398a2", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b5cf5e8-120f-4e7e-8123-80dcfd7f4191", "09952e55-42e3-4c75-884a-07099b570bee", "Administrator", "ADMINISTRATOR" });

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Vaqons_VaqonId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_VaqonId1",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4b5cf5e8-120f-4e7e-8123-80dcfd7f4191");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9e5d0330-ca6e-4a0f-8300-45b71243902a");

            migrationBuilder.AlterColumn<int>(
                name: "VaqonId1",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VaqonId2",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7818df44-10dc-45cf-af44-010094f40d9e", "3bd7b2d1-0757-48f9-9497-3f77a209cba0", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2a3c607-d2bf-4b95-ae14-696748be2a52", "d4ce6808-7b88-45aa-a0a8-c2a63474cb91", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VaqonId2",
                table: "Orders",
                column: "VaqonId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Vaqons_VaqonId2",
                table: "Orders",
                column: "VaqonId2",
                principalTable: "Vaqons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
