using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class sp_GetVaqonMilliBroker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetVaqonMB]
                    AS BEGIN
                        SELECT FilePath, DocType
                        FROM Vaqons
                        WHERE FilePath != NULL AND DocType != NULL
                    END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {}
    }
}
