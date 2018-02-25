SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************************************
* SP-NAME      : BT_Users_Sp																   *
* Author-NAME  : RajaSekar J															           *
* DESCRIPTION  : Get Work Users Detail from BT_Users_Sp Table						                           *
* DATE         : 24Feb18                                                                           *
***************************************************************************************************/
CREATE PROCEDURE [dbo].[BT_Users_Sp]
    @Operation INT,
    @intUserID INT = NULL,
    @intRoleID INT = NULL,
    @vchRFID VARCHAR(200) = NULL
AS
BEGIN
    IF (@Operation = 1)
    BEGIN
        SELECT *
        FROM dbo.Users_Ta;

    END;
    IF (@Operation = 2)
    BEGIN
        IF EXISTS (SELECT * FROM dbo.Users_Ta WHERE ([intUserID] = @intUserID))
        BEGIN
            UPDATE dbo.Users_Ta
            SET [intUserID] = @intUserID,
                [intRoleID] = @intRoleID,
                [vchRFID] = @vchRFID
            WHERE ([intUserID] = @intUserID);
        END;
        ELSE
        BEGIN
            INSERT INTO dbo.Users_Ta
            (
                [intUserID],
                [intRoleID],
                [vchRFID]
            )
            VALUES
            (@intUserID, @intRoleID, @vchRFID);

        END;
    END;
    IF (@Operation = 3)
    BEGIN
        DELETE FROM dbo.Users_Ta
        WHERE ([intUserID] = @intUserID);
    END;
END;
GO
