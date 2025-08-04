USE master;

RESTORE FILELISTONLY
FROM DISK = N'C:\RestauranteProgramacionII.bak';

USE master;


RESTORE DATABASE RestauranteProgramacionII
FROM DISK = N'C:\RestauranteProgramacionII.bak'
WITH
    MOVE N'RestauranteDBasePrueba' TO N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RestauranteProgramacionII_Data.mdf',
    MOVE N'RestauranteDBasePrueba_log' TO N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RestauranteProgramacionII_Log.ldf',
    REPLACE,
    STATS = 10;
GO


