CREATE TABLE [dbo].[Roles_Ta]
(
[intRoleID] [int] NOT NULL,
[vchRoleDescription] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Roles_Ta] ADD CONSTRAINT [PK_Roles_Ta] PRIMARY KEY CLUSTERED  ([intRoleID]) ON [PRIMARY]
GO
