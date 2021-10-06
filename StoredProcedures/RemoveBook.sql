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
begin try
BEGIN TRANSACTION;
if((select count(BookId) from MyOrders Where BookId=@BookId)=0)
begin
delete from Books Where BookId=@BookId
end
commit TRANSACTION;
end try
BEGIN CATCH
 -- Transaction uncommittable
    IF (XACT_STATE()) = -1
      ROLLBACK TRANSACTION
 
-- Transaction committable
    IF (XACT_STATE()) = 1
      COMMIT TRANSACTION
  END CATCH

  

