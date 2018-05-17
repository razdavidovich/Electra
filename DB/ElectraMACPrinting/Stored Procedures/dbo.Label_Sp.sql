SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************************************
* SP-NAME      : Label_Sp																		   *
* Author-NAME  : Prabakaran G															           *
* DESCRIPTION  : Get Label Data from tables.							                           *
* DATE         : 17May2018                                                                         *
***************************************************************************************************/
CREATE PROCEDURE [dbo].[Label_Sp]
    @Operation INT,
    @intLabelID INT = NULL,
    @vchLabelName VARCHAR(200) = NULL,
    @vchZPL VARCHAR(MAX) = NULL,
    @Key1 INT = NULL
AS
BEGIN
    IF (@Operation = 1)
    BEGIN
        SELECT *
        FROM dbo.Label_Ta;
    END;

    IF (@Operation = 2)
    BEGIN
        IF EXISTS (SELECT * FROM dbo.Label_Ta WHERE ([intLabelID] = @Key1))
        BEGIN
            UPDATE dbo.Label_Ta
            SET [vchLabelName] = @vchLabelName,
                [vchZPL] = @vchZPL
            WHERE ([intLabelID] = @Key1);
        END;
        ELSE
        BEGIN
            INSERT INTO dbo.Label_Ta
            (
                [vchLabelName],
                [vchZPL]
            )
            VALUES
            (@vchLabelName, @vchZPL);

        END;
    END;

    IF (@Operation = 3)
    BEGIN
        DELETE FROM dbo.Label_Ta
        WHERE ([intLabelID] = @Key1);
    END;

    IF (@Operation = 4)
    BEGIN
        SELECT intLabelID AS LabelID,
               vchLabelName AS LabelName,
               vchZPL AS ZPL
        FROM dbo.Label_Ta
        WHERE intLabelID = @Key1;
    END;

END;
GO
