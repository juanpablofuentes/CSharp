USE [SOM]
GO

SET IDENTITY_INSERT Languages ON
GO

INSERT INTO [dbo].[Languages]
           ([Id],
		   [UpdateDate]
           ,[Name]
           ,[CultureCode])
     VALUES
           (1,
		   GETUTCDATE()
           ,'Espa�ol'
           ,'es')
GO

INSERT INTO [dbo].[Languages]
           ([Id],
		   [UpdateDate]
           ,[Name]
           ,[CultureCode])
     VALUES
           (2,
		   GETUTCDATE()
           ,'Catal�'
           ,'ca')
GO

INSERT INTO [dbo].[Languages]
           ([Id],
		   [UpdateDate]
           ,[Name]
           ,[CultureCode])
     VALUES
           (3,
		   GETUTCDATE()
           ,'English'
           ,'en')
GO

SET IDENTITY_INSERT Languages OFF
GO

