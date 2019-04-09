CREATE PROCEDURE [dbo].[ENTUserAccountDelete]
	@ENTUserAccountId int
AS
	SET NOCOUNT ON
	DELETE
		FROM dbo.ENTUserAccount
		WHERE ENTUserAccountId = @ENTUserAccountId
RETURN
