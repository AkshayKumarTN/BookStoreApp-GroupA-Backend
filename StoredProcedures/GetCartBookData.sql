USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetCart]    Script Date: 29-09-2021 13:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetCart]
	@UserId int
AS
BEGIN

	select Cart.UserId, Cart.CartId, Cart.BookId,
	Books.Title,Books.BookDetail,Books.AuthorName,
	Books.BookImage,Books.BookQuantity,Books.Price,Books.Rating,
	Cart.BookCount, Cart.TotalCost from 
	Cart inner join Books on Cart.BookId = Books.BookId
	where Cart.UserId = @UserId;
END

