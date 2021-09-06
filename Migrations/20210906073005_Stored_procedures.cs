using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagerAPI.Migrations
{
    public partial class Stored_procedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // i created the stored procedures in the db
            // but i am not sure how to send them
            // so i just copied them here
            // BorcOrders
            var sql = @"CREATE OR ALTER PROCEDURE [dbo].[BorcOrders]
                        AS
                        BEGIN
	                        SELECT *
		                        FROM Orders
		                        WHERE EndDate IS NULL;
                        END";
            migrationBuilder.Sql(sql);

            // DeleteOrder
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[DeleteOrder]
	                    @Id int
                    AS
                    BEGIN
	                    DECLARE @VaqonId int
	                    SET @VaqonId = (
		                    SELECT VaqonId 
			                    FROM Orders 
			                    WHERE Id = @Id
	                    )
	                    DELETE FROM Orders
		                    WHERE Id = @Id
	                    DELETE FROM Vaqons
		                    WHERE Id = @VaqonId
                    END
                    ";
            migrationBuilder.Sql(sql);

            // DeleteVaqon
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[DeleteVaqon]
	                    @Id int
                    AS
                    BEGIN
	                    DELETE FROM Vaqons WHERE Id = @Id
                    END
                    ";
            migrationBuilder.Sql(sql);

            // GetOrders
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetOrders]
                    AS
                    BEGIN
                        -- Insert statements for procedure here
	                    SELECT * FROM Orders;
                    END;";
            migrationBuilder.Sql(sql);

            // GetOrdersBetweenStartAndEndDate
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetOrdersBetweenStartAndEndDate]
	                    @CreationDate DateTime2(7),
	                    @EndDate DateTime2(7)
                    AS
                    BEGIN
	                    SELECT * FROM ORDERS
		                    WHERE IsPaid = 1
		                      AND CreationDate > @CreationDate
		                      AND EndDate < @EndDate;
                    END";
            migrationBuilder.Sql(sql);

            // GetOrdersBiggerThanStartDate
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetOrdersBiggerThanStartDate]
	                    @CreationDate DateTime2(7)
                    AS
                    BEGIN
	                    SELECT * FROM ORDERS WHERE CreationDate > @CreationDate
                    END
                    ";
            migrationBuilder.Sql(sql);

            // GetUnusedVaqonIds
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetUnusedVaqonIds]
                    AS
                    BEGIN
	                    SELECT *
		                    FROM Vaqons
		                    WHERE OrderId IS NULL;
                    END";
            migrationBuilder.Sql(sql);

            // GetVaqonMBKWithoutOrderId
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetVaqonMBKWithoutOrderId]
                    AS
                    BEGIN
	                    SELECT * 
		                    FROM Vaqons
		                    WHERE OrderId IS NULL 
		                      AND FilePath IS NULL
		                      AND DocType IS NULL
		                      AND Type IS NOT NULL;
                    END";
            migrationBuilder.Sql(sql);

            // GetVaqonMBWithoutOrderId
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetVaqonMBWithoutOrderId]
                    AS
                    BEGIN
	                    SELECT * 
		                    FROM Vaqons
		                    WHERE OrderId IS NULL 
		                      AND TYPE IS NULL
		                      AND FilePath IS NOT NULL
		                      AND DocType IS NOT NULL
                    END";
            migrationBuilder.Sql(sql);

            // GetVaqons
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetVaqons]
                    AS
                    BEGIN
	                    SELECT * FROM Vaqons
                    END
                    ";
            migrationBuilder.Sql(sql);

            // GetVaqonsMB
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetVaqonsMB]
                    AS
                    BEGIN
	                    SELECT *
	                    FROM Vaqons
	                    WHERE FilePath IS NOT NULL AND DocType IS NOT NULL;
                    END
                    ";
            migrationBuilder.Sql(sql);

            // GetVaqonsMBK
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetVaqonsMBK]
                    AS
                    BEGIN
	                    SELECT *
	                    FROM Vaqons
	                    WHERE Type IS NOT NULL;
                    END";
            migrationBuilder.Sql(sql);

            // InsertOrder
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[InsertOrder]
	                    -- Add the parameters for the stored procedure here
	                    @Xidmet INT,
	                    @Vahid INT,
	                    @Miqdar INT,
	                    @Qiymet Decimal(18,2),
	                    @CreationDate DateTime2(7),
	                    @VaqonId INT,
	                    @OrderId INT OUTPUT
                    AS
                    BEGIN
                        -- Insert statements for procedure here
	                    INSERT INTO Orders(Xidmet, Vahid, Miqdar, Qiymet, CreationDate, VaqonId, IsPaid)
	                    VALUES(@Xidmet, @Vahid, @Miqdar, @Qiymet, @CreationDate, @VaqonId, 0);
	                    SET @OrderId = (
		                    SELECT SCOPE_IDENTITY()
	                    );
                    END;";
            migrationBuilder.Sql(sql);

            // InsertVaqonMB
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[InsertVaqonMB]
                        ( @FilePath NVARCHAR(MAX),
                        @DocType INT)
                    AS BEGIN
                        INSERT INTO Vaqons(FilePath, DocType)
                        VALUES(@FilePath, @DocType)
                    END";
            migrationBuilder.Sql(sql);

            // InsertVaqonMBK
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[InsertVaqonMBK]
	                    -- Add the parameters for the stored procedure here
	                    @Type int
                    AS
                    BEGIN
	                    -- SET NOCOUNT ON added to prevent extra result sets from
	                    -- interfering with SELECT statements.
	                    SET NOCOUNT ON;

                        -- Insert statements for procedure here
	                    INSERT INTO Vaqons(Type)
	                    Values(@Type);
                    END;";
            migrationBuilder.Sql(sql);
            // PayOrder
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[PayOrder]
	                    @OrderId int,
	                    @EndDate datetime2(7)
                    AS
                    BEGIN
	                    UPDATE Orders
	                    SET EndDate = @EndDate, IsPaid = 1
	                    WHERE Id = @OrderId;
                    END";
            migrationBuilder.Sql(sql);

            // UpdateOrderIdOfVaqonId
            sql = @"CREATE OR ALTER PROCEDURE [dbo].[UpdateOrderIdOfVaqonId]
	                    @OrderId INT
                    AS
                    BEGIN
	                    DECLARE @VaqonId INT
	                    --Get vaqon id from the order
	                    SET @VaqonId = (
		                    SELECT VaqonId 
		                    FROM Orders 
		                    WHERE Id = @OrderId
	                    );
	                    --update the order id of the vaqon
	                    UPDATE Vaqons
	                    SET OrderId = @OrderId
	                    WHERE Id = @VaqonId;
                    END";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROC BorcOrders";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC DeleteOrder";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC DeleteVaqon";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetOrders";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetOrdersBetweenStartAndEndDate";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetOrdersBiggerThanStartDate";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetUnusedVaqonIds";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetVaqonMBKWithoutOrderId";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetVaqonMBWithoutOrderId";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetVaqons";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetVaqonsMB";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC GetVaqonsMBK";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC InsertOrder";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC InsertVaqonMB";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC InsertVaqonMBK";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC PayOrder";
            migrationBuilder.Sql(sql);

            sql = "DROP PROC UpdateOrderIdOfVaqonId";
            migrationBuilder.Sql(sql);
        }
    }
}
