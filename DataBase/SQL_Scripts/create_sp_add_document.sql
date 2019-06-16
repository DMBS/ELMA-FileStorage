USE [ELMA]
GO
CREATE PROCEDURE [dbo].[sp_add_document]
@DocumentName VARCHAR(50),
@DocumentType VARCHAR(50),
@DocumentAuthor VARCHAR(50),
@DocumentBinaryFile VARBINARY(max)
as
begin
INSERT INTO Documents (DocumentName,DocumentType, DocumentAuthor,DocumentBinaryFile)
VALUES(@DocumentName,@DocumentType,@DocumentAuthor,@DocumentBinaryFile)
end;
