Create PROCEDURE [dbo].[DecreaseCount]
@CartId int,
@BookId int
As
BEGIN
	DECLARE @BookPrice int = 0;
	set @BookPrice= (select Price from Books where BookId = @BookId); 
      UPDATE Cart set Cart.BookCount = Cart.BookCount - 1,Cart.TotalCost=Cart.TotalCost-@BookPrice 
	  where CartId=@CartId and BookCount>1;
END