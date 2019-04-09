CREATE PROCEDURE [dbo].[ENTUserAccountSelectById]
	@ENTUserAccountId int
AS
	SET NOCOUNT ON
  
	SELECT ENTUserAccountId, WindowsAccountName, FirstName, LastName, 
             Email, IsActive, InsertDate, InsertENTUserAccountid, 
             UpdateDate, UpdateENTUserAccountId, Version
    FROM ENTUserAccount
   WHERE ENTUserAccountId = @ENTUserAccountId
  
 RETURN