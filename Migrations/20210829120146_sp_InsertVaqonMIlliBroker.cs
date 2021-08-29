using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class sp_InsertVaqonMIlliBroker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[InsertVaqonMB]
                        ( @FilePath NVARCHAR(MAX),
                        @DocType INT )
                    AS BEGIN
                        INSERT INTO Vaqons(FilePath, DocType)
                        VALUES(@FilePath, @DocType)
                    END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        
        }
    }
}
