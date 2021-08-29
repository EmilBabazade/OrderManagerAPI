using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class sp_GetVaqons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetVaqons]
                    AS BEGIN
                        select * from Vaqons
                    END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        
        }
    }
}
