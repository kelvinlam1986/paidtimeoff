CREATE TABLE [dbo].[ENTMenuItem]
(
	[ENTMenuItemId] INT NOT NULL PRIMARY KEY, 
    [MenuItemName] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(MAX) NULL, 
    [Url] VARCHAR(MAX) NULL, 
    [ParentENTMenuItemId] INT NULL, 
    [DisplaySequence] TINYINT NOT NULL, 
    [IsAlwaysEnabled] BIT NOT NULL, 
    [InsertDate] DATETIME NOT NULL DEFAULT getdate(), 
    [InsertENTUserAccountId] INT NOT NULL, 
    [UpdateDate] DATETIME NOT NULL DEFAULT getdate(), 
    [UpdateENTUserAccountId] INT NOT NULL, 
    [Version] TIMESTAMP NOT NULL
)
