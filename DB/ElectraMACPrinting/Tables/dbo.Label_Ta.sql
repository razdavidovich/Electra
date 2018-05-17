CREATE TABLE [dbo].[Label_Ta]
(
[intLabelID] [int] NOT NULL IDENTITY(1, 1),
[vchLabelName] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[vchZPL] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Label_Ta] ADD CONSTRAINT [PK_Label_Ta] PRIMARY KEY CLUSTERED  ([intLabelID]) ON [PRIMARY]
GO
