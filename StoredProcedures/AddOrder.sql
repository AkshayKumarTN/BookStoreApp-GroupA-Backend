USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[AddOrder]    Script Date: 02-10-2021 03:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddOrder]
	@UserId int,
	@BookId int,
	@AddressId int,
	@OrderDate varchar(20),
	@TotalCost int
	AS
BEGIN TRY 
BEGIN TRANSACTION
    DECLARE @BookPrice int = 0;
	DECLARE @BookCount int =1;
	DECLARE @TotalBookCount int = 1;
	set @BookPrice= (select Price from Books where BookId = @BookId); 
	Set @BookCount = (@TotalCost/@BookPrice);
	Set @TotalBookCount =(select BookQuantity from Books where BookId = @BookId); 
if( (select count(BookId) from Books where BookId = @BookId and BookQuantity > @BookCount) = 1)
		Begin
				insert into MyOrders(UserId, BookId, AddressId,OrderDate,TotalCost) 
				values(@UserId, @BookId,@AddressId,@OrderDate,@TotalCost);
				update Books set BookQuantity=@TotalBookCount-@BookCount where BookId = @BookId;
				select OrderId from MyOrders where BookId=@BookId and UserId = @UserId and AddressId=@AddressId Order By OrderId desc;
		End
	COMMIT TRANSACTION
END TRY
 BEGIN CATCH
 -- Transaction uncommittable
    IF (XACT_STATE()) = -1
      ROLLBACK TRANSACTION
 
-- Transaction committable
    IF (XACT_STATE()) = 1
      COMMIT TRANSACTION
  END CATCH