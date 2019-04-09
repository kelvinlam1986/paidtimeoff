CREATE PROCEDURE [dbo].[ENTUserAccountInsert]
	@ENTUserAccountId int OUTPUT,
	@WindowsAccountName varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@Email varchar(100),
	@IsActive bit,
	@InsertENTUserAccountId int
AS
	SET NOCOUNT ON
	INSERT INTO [dbo].[ENTUserAccount](WindowsAccountName, FirstName, LastName, 
            Email, IsActive, InsertENTUserAccountId, InsertDate, 
            UpdateENTUserAccountId, UpdateDate)
     VALUES (@WindowsAccountName, @FirstName, @LastName, @Email, 
            @IsActive, @InsertENTUserAccountId, GetDate(), 
            @InsertENTUserAccountId, GetDate())
	SET @ENTUserAccountId = SCOPE_IDENTITY()
RETURN