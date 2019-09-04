USE [SOM]
GO

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Billable', 'Billable', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'FinalClient', 'ClientFinal', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Queue', 'Cua', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'WorkOrderStatus', 'EstatOT', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'ExternalWorkOrderStatus', 'EstatOTExtern', GETUTCDATE())
	 
INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Project', 'Project', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'State', 'State', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Technician', 'Tecnic', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN1', 'TipusOTN1', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN2', 'TipusOTN2', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN3', 'TipusOTN3', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN4', 'TipusOTN4', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'TypeOTN5', 'TipusOTN5', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'FinalClientLocation', 'UbicacioClientFinal', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'WOCategory', 'WOCategory', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Zone', 'Zone', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Zone', 'MinutsPerFiSLA', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'CreateDate', 'DataCreacio', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'AssignmentDate', 'DataAssignacio', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'CollectionDate', 'DataRecollida', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'ActuationDate', 'DataActuacio', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'SaltoClosureDate', 'DataTancamentSalto', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'ClientClosureDate', 'DataTancamentClient', GETUTCDATE())

INSERT INTO [dbo].[PreconditionTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'WOClientClosureDate', 'DataTancamentOTClient', GETUTCDATE())
GO