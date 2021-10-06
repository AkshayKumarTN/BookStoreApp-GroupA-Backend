ALTER PROCEDURE [dbo].[Registration]
	@FullName		varchar(50),
	@EmailId		varchar(50),
	@Password		varchar(50),
	@MobileNumber   bigint,
	 @result int output
AS
BEGIN TRY
IF((select count(*) from [User] where EmailId = @EmailId ) = 0)
BEGIN
  Insert into [User](FullName,EmailId,Password,MobileNumber)
	VALUES(@FullName,@EmailId,@Password,@MobileNumber);
	set @result =1;
END

ELSE
BEGIN
   set @result=2;
   throw 5000,'Email Id already exist',2;
END
END TRY
BEGIN CATCH 
set @result=0
SELECT ERROR_STATE() AS ErrorState , ERROR_MESSAGE() ErrorMsg ;  
END CATCH; 
