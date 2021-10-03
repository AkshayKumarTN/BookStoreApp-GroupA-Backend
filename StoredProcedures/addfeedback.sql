create PROCEDURE [dbo].[AddFeedBackData]
@BookId int,
@UserName varchar(255),
@Rating int,
@Comments varchar(max)
as
begin
Insert into [AddFeedBack](BookId,UserName,Rating,Comments)
Values (@BookId,@UserName,@Rating,@Comments)
end
