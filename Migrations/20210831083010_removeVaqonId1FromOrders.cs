using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class removeVaqonId1FromOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("VaqonId1", "Orders");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4b5cf5e8-120f-4e7e-8123-80dcfd7f4191");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9e5d0330-ca6e-4a0f-8300-45b71243902a");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f7a14655-e874-46b3-839a-2cec561da1e9", "124e105c-3fa3-4ec1-9d5c-965e7734e71f", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18b5a8ce-342e-466c-b05d-c729df58c8b8", "4bbc7bbf-7974-41c5-991e-32dba5fe409d", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "18b5a8ce-342e-466c-b05d-c729df58c8b8");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f7a14655-e874-46b3-839a-2cec561da1e9");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e5d0330-ca6e-4a0f-8300-45b71243902a", "95c4ac12-2045-4bda-b9ba-8dc754a398a2", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b5cf5e8-120f-4e7e-8123-80dcfd7f4191", "09952e55-42e3-4c75-884a-07099b570bee", "Administrator", "ADMINISTRATOR" });
        }
    }
}
