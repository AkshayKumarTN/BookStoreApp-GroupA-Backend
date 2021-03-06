ALTER PROCEDURE[dbo].[UpdateBookData]
@BookId  int,
	@Title	varchar(255),
	@AuthorName	varchar(255),
	@Price	int,
	@Rating   int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BigBookImage varchar(max),
	@BookQuantity int,
	@result int output
AS
BEGIN
begin try
set xact_abort on;
begin transaction;
IF(@BookImage = 'update' OR @BigBookImage  = 'update')
BEGIN
SET @BookImage = (select BookImage from[Books] where BookId =@BookId)
SET @BigBookImage = (select BigImage from[Books] where BookId =@BookId)
End
if ((select count(*) from[Books] where BookId = @BookId) = 1)
begin
		UPDATE Books set Title = @Title, AuthorName = @AuthorName,
Price = @Price,
Rating = @Rating, BookDetail = @BookDetail, BookImage = @BookImage, BigImage = @BigBookImage,
BookQuantity =@BookQuantity
	 where BookId = @BookId;
select * from[Books] where BookId =@BookId;
set @result = 1;
end;
	commit transaction;
end try
begin catch
if (xact_state())= -1
begin
print N'The querys have some error!!' + 'Rolling back transaction'
rollback transaction;
end;
if (xact_state())= 1
begin
set @result = 1
print N'update Book successfull'
commit transaction;
end;
end catch
END