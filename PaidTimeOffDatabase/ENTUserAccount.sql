CREATE TABLE [dbo].[ENTUserAccount]
(
	[ENTUserAccountId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WindowsAccountName] VARCHAR(50) NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(100) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [InsertDate] DATETIME NOT NULL DEFAULT getdate(), 
    [InsertENTUserAccountId] INT NOT NULL, 
    [UpdateDate] DATETIME NOT NULL DEFAULT getdate(), 
    [UpdateENTUserAccountId] INT NOT NULL, 
    [Version] TIMESTAMP NULL
)
