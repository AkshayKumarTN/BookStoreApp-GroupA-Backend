USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetBook]    Script Date: 29-09-2021 14:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetBook]

@BookId int
AS
BEGIN
if((select count(*) from [Books] where BookId = @BookId) = 1)
		BEGIN
		select * from [Books] where BookId = @BookId;
		END
END