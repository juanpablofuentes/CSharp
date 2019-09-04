USE [SOM]
GO

INSERT INTO [dbo].[ReferenceTimeSla]([UpdateDate],[Id],[Name],[Description])VALUES(GETUTCDATE(),NEWID(),'DataActuacio')
INSERT INTO [dbo].[ReferenceTimeSla]([UpdateDate],[Id],[Name],[Description])VALUES(GETUTCDATE(),NEWID(),'DataCreacio')
INSERT INTO [dbo].[ReferenceTimeSla]([UpdateDate],[Id],[Name],[Description])VALUES(GETUTCDATE(),NEWID(),'DataAssignacio')
INSERT INTO [dbo].[ReferenceTimeSla]([UpdateDate],[Id],[Name],[Description])VALUES(GETUTCDATE(),NEWID(),'DataRecullidaInterna')


GO