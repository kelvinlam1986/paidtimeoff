CREATE PROCEDURE [dbo].[ENTUserAccountSelectAll]
AS
	SET NOCOUNT ON

	SELECT ENTUserAccountId, WindowsAccountName, FirstName, LastName,
			Email, IsActive, InsertDate, InsertENTUserAccountId,
			UpdateDate, UpdateENTUserAccountId, Version
	FROM dbo.ENTUserAccount
RETURN 0
