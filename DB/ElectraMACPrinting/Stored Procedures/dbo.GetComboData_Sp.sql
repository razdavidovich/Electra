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
        SELECT intRoleID AS [Value],
               vchRoleDescription AS [Text]
        FROM dbo.Roles_Ta;
    END;

    IF (2 = @intTable)
    BEGIN

        SELECT intLabelID AS [Value],
               vchLabelName AS [Text]
        FROM dbo.Label_Ta;
    END;


    SET NOCOUNT OFF;
END;
GO
