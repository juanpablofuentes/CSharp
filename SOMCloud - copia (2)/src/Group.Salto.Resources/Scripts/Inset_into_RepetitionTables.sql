USE [SOM]
GO
INSERT INTO [CalculationTypes] ([Id], [Name], [NumDays], [UpdateDate]) VALUES (NEWID(), 'MENSUA', 30, GETUTCDATE())
INSERT INTO [CalculationTypes] ([Id], [Name], [NumDays], [UpdateDate]) VALUES (NEWID(), 'BIMESTRAL', 60, GETUTCDATE())

INSERT INTO [DamagedEquipment] ([Id], [Name], [UpdateDate]) VALUES (NEWID(), 'NUM_SÈRIE', GETUTCDATE())
INSERT INTO [DamagedEquipment] ([Id], [Name], [UpdateDate]) VALUES (NEWID(), 'ELEMENT', GETUTCDATE())

INSERT INTO [DaysTypes] ([Id], [Name], [UpdateDate]) VALUES (NEWID(), 'FESTIUS', GETUTCDATE())
INSERT INTO [DaysTypes] ([Id], [Name], [UpdateDate]) VALUES (NEWID(), 'LABORALS', GETUTCDATE())
GO

Use [SOMSalto]
GO
INSERT INTO [RepetitionParameters] ([Id], [Days], [IdCalculationType], [IdDamagedEquipment], [IdDaysType], [UpdateDate]) VALUES (NEWID(), 30, '197806EF-4000-40E8-AD0F-6842C69F1E5A', 'F9B7CB06-5AE2-42C1-9433-404BD03EE49F', 'AEDE562F-84E3-46ED-BCDD-6A9954AEE4D1', GETUTCDATE())
GO
