USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[RemoveBook]    Script Date: 05-10-2021 11:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[RemoveBook]
@BookId int
As
begin 
delete from Books where BookId=@BookId;
end;