USE [ELMA]
GO
/****** Object:  StoredProcedure [dbo].[sp_add_user]    Script Date: 2/10/2018 9:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_add_user]
@UserName nvarchar(50),
@Password nvarchar(50),
@Email nvarchar(50)
AS
BEGIN
	INSERT INTO Users ([UserName],[Password],[Email])
	VALUES (@UserName,@Password,@Email)
END