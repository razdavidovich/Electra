SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************************************
* SP-NAME      : Login_Sp																		   *
* Author-NAME  : RajaSekar J														           *
* DESCRIPTION  : Validate the User Login and get the Details .			                           *
* DATE         : 24Feb18                                                                           *
***************************************************************************************************/
CREATE PROCEDURE [dbo].[Login_Sp]
    -- Add the parameters for the stored procedure here
    @vchRFID VARCHAR(200)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- SELECT statements for procedure here
    SELECT dbo.Users_Ta.intUserID,
           dbo.Users_Ta.intRoleID,
           dbo.Roles_Ta.vchRoleDescription
    FROM dbo.Users_Ta
        INNER JOIN dbo.Roles_Ta
            ON dbo.Users_Ta.intRoleID = dbo.Roles_Ta.intRoleID
    WHERE dbo.Users_Ta.vchRFID = @vchRFID;

    SET NOCOUNT OFF;
END;
GO
