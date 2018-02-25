SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

/***************************************************************************************************
* SP-NAME      : BT_Roles_Sp																   *
* Author-NAME  : RajaSekar J															           *
* DESCRIPTION  : Get Work Roles Detail from BT_Roles_Sp Table						                           *
* DATE         : 24Feb18                                                                           *
***************************************************************************************************/
CREATE PROCEDURE [dbo].[BT_Roles_Sp]
    @Operation INT,
    @intRoleID INT = NULL,
    @vchRoleDescription VARCHAR(50) = NULL

AS
BEGIN
    IF (@Operation = 1)
    BEGIN
        SELECT *
        FROM dbo.Roles_Ta;
    END;
    IF (@Operation = 2)
    BEGIN
        IF EXISTS (SELECT * FROM dbo.Roles_Ta WHERE ([intRoleID] = @intRoleID))
        BEGIN
            UPDATE dbo.Roles_Ta
            SET [intRoleID] = @intRoleID,
                [vchRoleDescription] = @vchRoleDescription
            WHERE ([intRoleID] = @intRoleID);
        END;
        ELSE
        BEGIN
            INSERT INTO dbo.Roles_Ta
            (
                [intRoleID],
                [vchRoleDescription]
            )
            VALUES
            (@intRoleID, @vchRoleDescription);

        END;
    END;
    IF (@Operation = 3)
    BEGIN
        DELETE FROM dbo.Roles_Ta
        WHERE ([intRoleID] = @intRoleID);
    END;
END;
GO
