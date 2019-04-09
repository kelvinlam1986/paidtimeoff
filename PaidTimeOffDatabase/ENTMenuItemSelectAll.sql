CREATE PROCEDURE [dbo].[ENTMenuItemSelectAll]
AS
	SET NOCOUNT ON
	SELECT ENTMenuItemId, MenuItemName, Description, Url, ParentENTMenuItemId, 
           DisplaySequence, IsAlwaysEnabled, InsertDate, InsertENTUserAccountId, 
           UpdateDate, UpdateENTUserAccountId, Version
	FROM dbo.ENTMenuItem
	ORDER BY ParentENTMenuItemId, DisplaySequence, MenuItemName
RETURN 0
