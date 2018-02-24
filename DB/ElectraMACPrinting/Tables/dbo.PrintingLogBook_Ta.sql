CREATE TABLE [dbo].[PrintingLogBook_Ta]
(
[intRowID] [int] NOT NULL IDENTITY(1, 1),
[datPrintingDate] [datetime] NULL,
[vchMachine] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[intUserID] [int] NULL,
[vchSerialNumber] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[vchMACAddress] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrintingLogBook_Ta] ADD CONSTRAINT [PK_MarkingLogBook_Ta] PRIMARY KEY CLUSTERED  ([intRowID]) ON [PRIMARY]
GO
