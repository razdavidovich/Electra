CREATE TABLE [dbo].[Users_Ta]
(
[intUserID] [int] NOT NULL,
[intRoleID] [int] NOT NULL,
[vchRFID] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users_Ta] ADD CONSTRAINT [PK_Users_Ta] PRIMARY KEY CLUSTERED  ([intUserID]) ON [PRIMARY]
GO
