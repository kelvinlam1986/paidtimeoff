CREATE PROCEDURE [dbo].[ENTUserAccountUpdate]
	@ENTUserAccountId int,
    @WindowsAccountName varchar(50),
    @FirstName varchar(50),
    @LastName varchar(50),
    @Email varchar(100),
    @IsActive bit,
    @UpdateENTUserAccountId int,
    @Version timestamp
AS
	SET NOCOUNT ON

	UPDATE ENTUserAccount
     SET WindowsAccountName = @WindowsAccountName,
         FirstName = @FirstName,
         LastName = @LastName,
         Email = @Email,
         IsActive = @IsActive,
         UpdateENTUserAccountId = @UpdateENTUserAccountId,
         UpdateDate = GetDate()
   WHERE ENTUserAccountId = @ENTUserAccountId
     AND Version = @Version

RETURN @@ROWCOUNT
