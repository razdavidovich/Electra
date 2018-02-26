SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************************************
* SP-NAME      : GetComboData_Sp																   *
* Author-NAME  : RajaSekar J															           *
* DESCRIPTION  : Get Combo Data from tables.							                           *
* DATE         : 26Feb18                                                                           *
***************************************************************************************************/
CREATE PROCEDURE [dbo].[GetComboData_Sp] @intTable INT
AS
BEGIN
    SET NOCOUNT ON;

    IF (@intTable = 1)
    BEGIN
        SELECT intRoleID AS [value],
               vchRoleDescription AS [Text]
        FROM dbo.Roles_Ta;
    END;

    IF (2 = @intTable)
    BEGIN

        SELECT [intPartNumber] AS [value],
               [intPartNumber] AS [Text]
        FROM [PartNumbers_Ta];
    END;

	 IF (3 = @intTable)
    BEGIN

        SELECT [intParameterID] AS [value],
               [vchParameterName] AS [Text]
        FROM [Parameters_Ta];
    END;

    SET NOCOUNT OFF;
END;
GO
