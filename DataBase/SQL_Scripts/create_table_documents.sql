USE [ELMA]
GO
CREATE TABLE [dbo].[Documents](
	[DocumentId] [int] IDENTITY (1,1) NOT NULL,
	[DocumentName] [varchar](50) NOT NULL,
	[DocumentType] [varchar](50) NOT NULL,
	[DocumentDate] [DATETIME] NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[DocumentAuthor] [varchar](50) NOT NULL,
	[DocumentBinaryFile] [varbinary](max) NOT NULL
	CONSTRAINT PK_DocumentID PRIMARY KEY CLUSTERED(DocumentId)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


