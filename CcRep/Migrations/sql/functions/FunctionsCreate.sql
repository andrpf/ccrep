-- Ф-ция, котоая проверяет, что SQL Server не является Express Edition
CREATE FUNCTION CheckNotExpress ()  
RETURNS BIT  
AS  
BEGIN

DECLARE @Result BIT = 0;

DECLARE @SqlEdition NVARCHAR(50) = (SELECT CAST(SERVERPROPERTY('edition') AS NVARCHAR(MAX)));

IF (SELECT CHARINDEX('Express', @SqlEdition)) = 0
	SET @Result = 1;

RETURN @Result;
END;

GO

CREATE PROCEDURE AddTableToCdc @tableName NVARCHAR(MAX), @schema NVARCHAR(MAX)
AS  
BEGIN

EXEC sys.sp_cdc_enable_table  
@source_schema = @schema,  
@source_name   = @tableName,  
@role_name     = N'public',  
@supports_net_changes = 1
-- @captured_column_list = N'id,name,family'

END;

GO