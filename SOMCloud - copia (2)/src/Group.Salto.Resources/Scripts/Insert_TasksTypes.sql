USE [SOM]
GO

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Closure', 'Cierre', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Intervention', 'Intervencion', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Logistics', 'Logistica', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Operator', 'Operario', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Creation', 'Creacion', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Administration', 'Administracion', GETUTCDATE())

INSERT INTO [dbo].[TriggerTypes] ([Id],[Name],[Description], [UpdateDate])
     VALUES (NEWID(), 'Workshop', 'Taller', GETUTCDATE())
GO

