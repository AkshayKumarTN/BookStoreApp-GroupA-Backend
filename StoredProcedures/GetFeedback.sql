
  Create Procedure [dbo].[GetFeedBack]
  @BookId int
  as
  begin
  select * from AddFeedBack where BookId=@BookId;
  end