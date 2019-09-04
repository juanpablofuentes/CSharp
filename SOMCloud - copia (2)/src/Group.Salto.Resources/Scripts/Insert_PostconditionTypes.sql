USE [SOM]
GO

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Billable', 'Billable', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'FinalClient', 'ClientFinal', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Queue', 'Cua', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'WorkOrderStatus', 'EstatOT', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'ExternalWorkOrderStatus', 'EstatOTExtern', GETUTCDATE())
	 
INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Project', 'Project', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'State', 'State', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Technician', 'Tecnic', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN1', 'TipusOTN1', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN2', 'TipusOTN2', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN3', 'TipusOTN3', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN4', 'TipusOTN4', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN5', 'TipusOTN5', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'FinalClientLocation', 'UbicacioClientFinal', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'WOCategory', 'WOCategory', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Zone', 'Zone', GETUTCDATE())
GO