using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class OrderVaqonModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a01f2219-6a02-4564-b6e1-1f105548bbb3");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fe73f0dd-7f46-4999-aa03-85485cae5ec7");

            migrationBuilder.CreateTable(
                name: "Vaqons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(nullable: true),
                    DocType = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaqons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Xidmet = table.Column<int>(nullable: false),
                    Vahid = table.Column<int>(nullable: false),
                    Miqdar = table.Column<int>(nullable: false),
                    Qiymet = table.Column<decimal>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    VaqonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Vaqons_VaqonId",
                        column: x => x.VaqonId,
                        principalTable: "Vaqons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b4661de9-221c-429b-97d4-de375b716122", "d31a16c4-3d18-4c6b-99cb-25801602584a", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "002993ad-0f3f-4c17-bd80-dda8c44e9501", "43e8aa2f-54e5-499e-a882-263c6011d5cd", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VaqonId",
                table: "Orders",
                column: "VaqonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Vaqons");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "002993ad-0f3f-4c17-bd80-dda8c44e9501");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b4661de9-221c-429b-97d4-de375b716122");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe73f0dd-7f46-4999-aa03-85485cae5ec7", "db419d9c-b36b-43bc-8aaf-bb6a08dbb424", "Standart", "STANDART" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a01f2219-6a02-4564-b6e1-1f105548bbb3", "2d52497f-da04-44c3-a07a-055ca5b46734", "Administrator", "ADMINISTRATOR" });
        }
    }
}
