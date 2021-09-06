using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class sp_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // test to see how sp creation from migration works
            var sql = @"
                CREATE OR ALTER PROCEDURE [dbo].[TESTESTESTESTEST]
                AS
                BEGIN
	                SELECT *
		                FROM Orders
		                WHERE EndDate IS NULL;
                END

            ";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROC TESTESTESTESTEST";
            migrationBuilder.Sql(sql);
        }
    }
}
