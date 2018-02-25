SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************************************
* SP-NAME      : TranslationGlossary_SP															   *
* Author-NAME  : Prabakaran G															           *
* DESCRIPTION  : Get the Languages based text from TranslationGlossary_Ta table.			       *
* DATE         : 10Aug2015                                                                           *
***************************************************************************************************/
CREATE PROCEDURE [dbo].[TranslationGlossary_SP]
    -- Add the parameters for the stored procedure here
    @Operation INT,
    @vchLanguageCode VARCHAR(5) = NULL,
    @vchValues VARCHAR(MAX) = NULL
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    IF (1 = @Operation)
    BEGIN
        SELECT vchKey,
               vchLanguageCode,
               vchTranslation
        FROM dbo.TranslationGlossary_Ta
        WHERE vchLanguageCode = @vchLanguageCode;
    END;



    SET NOCOUNT OFF;

END;
GO
