SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************************************
* SP-NAME      : PrintingLogBook_Sp																   *
* Author-NAME  : RajaSekar J															           *
* DESCRIPTION  : Get Work LogBook Detail from PrintingLogBook_Ta Table						                           *
* DATE         : 24Feb18                                                                           *
***************************************************************************************************/
CREATE PROCEDURE [dbo].[PrintingLogBook_Sp]
    @Operation INT,
    @intRowID INT = NULL,
    @datPrintingDate DATETIME = NULL,
    @vchMachine VARCHAR(50) = NULL,
    @intUserID INT = NULL,
    @vchSerialNumber VARCHAR(50) = NULL,
    @vchMACAddress VARCHAR(50) = NULL,
    @datFromDate DATETIME = NULL,
    @datToDate DATETIME = NULL
AS
BEGIN
    IF (@Operation = 1)
    BEGIN
        SELECT *
        FROM dbo.PrintingLogBook_Ta;
    END;
    IF (@Operation = 2)
    BEGIN
        IF EXISTS
        (
            SELECT *
            FROM dbo.PrintingLogBook_Ta
            WHERE ([intRowID] = @intRowID)
        )
        BEGIN
            UPDATE dbo.PrintingLogBook_Ta
            SET [datPrintingDate] = @datPrintingDate,
                [vchMachine] = @vchMachine,
                [intUserID] = @intUserID,
                [vchSerialNumber] = @vchSerialNumber,
                [vchMACAddress] = @vchMACAddress
            WHERE ([intRowID] = @intRowID);
        END;
        ELSE
        BEGIN
            INSERT INTO dbo.PrintingLogBook_Ta
            (
                [datPrintingDate],
                [vchMachine],
                [intUserID],
                [vchSerialNumber],
                [vchMACAddress]
            )
            VALUES
            (@datPrintingDate, @vchMachine, @intUserID, @vchSerialNumber, @vchMACAddress);

        END;
    END;
    IF (@Operation = 3)
    BEGIN
        DELETE FROM dbo.PrintingLogBook_Ta
        WHERE ([intRowID] = @intRowID);
    END;
    IF (@Operation = 4)
    BEGIN
        SELECT datPrintingDate,
               vchMachine,
               intUserID,
               vchSerialNumber,
               vchMACAddress
        FROM dbo.PrintingLogBook_Ta
        WHERE CAST(datPrintingDate AS DATE)
        BETWEEN CAST(@datFromDate AS DATE) AND CAST(@datToDate AS DATE);
    END;
END;
GO
