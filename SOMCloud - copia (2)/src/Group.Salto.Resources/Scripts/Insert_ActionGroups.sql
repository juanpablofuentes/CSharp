USE [SOM]
GO

INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'Actions','Actions',GETUTCDATE())
INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'Projects','Projects',GETUTCDATE())
INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'Persons','Persons',GETUTCDATE())
INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'Knowledges','Knowledges',GETUTCDATE())
INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'Assets','Assets',GETUTCDATE())
INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'Vehicles','Vehicles',GETUTCDATE())
INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'ToolTypes','ToolTypes',GETUTCDATE())
INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'Queues','Queues',GETUTCDATE())
INSERT INTO [dbo].[ActionGroups]([Id],[Name],[Description],[UpdateDate]) VALUES(NEWID(),'WorkOrderCategories','WorkOrderCategories',GETUTCDATE())