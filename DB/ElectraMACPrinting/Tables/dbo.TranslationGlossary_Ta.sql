CREATE TABLE [dbo].[TranslationGlossary_Ta]
(
[vchKey] [varchar] (500) COLLATE Latin1_General_CI_AI NOT NULL,
[vchLanguageCode] [varchar] (2) COLLATE Latin1_General_CI_AI NOT NULL,
[vchTranslation] [nvarchar] (500) COLLATE Latin1_General_CI_AI NOT NULL CONSTRAINT [DF_TranslationGlossary_Ta_vchTranslation] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TranslationGlossary_Ta] ADD CONSTRAINT [PK_TranslationGlossary_Ta] PRIMARY KEY CLUSTERED  ([vchKey]) ON [PRIMARY]
GO
