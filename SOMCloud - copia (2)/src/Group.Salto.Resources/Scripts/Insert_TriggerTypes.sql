USE [SOM]
GO

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'AccountingClosingDate', 'AccountingClosingDate', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(),'ActionDate', 'DataActuacio', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'AssignmentDate', 'DataAsignacio', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Creation', 'Creacio', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(),'ClosureClientDate', 'DataTancamentClient', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'PredefinedServiceId', 'IdServeiPredefinit', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'ReopenOT', 'ReopenOT', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'RestartSLAWatch', 'RestartSLAWatch', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'StopSLAWatch', 'StopSLAWatch', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TechnicianAndActuationDate', 'TechnicianAndActuationDate', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Technician', 'Tecnic', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id], [Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'NoAction', 'NoAction', GETUTCDATE())
GO