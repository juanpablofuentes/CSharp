GO
 

insert into CalculationTypes (UpdateDate, Id, [Name], NumDays) values (GETUTCDATE(),NEWID(), 'Mensual', 30);

insert into CalculationTypes (UpdateDate, Id, [Name], NumDays) values (GETUTCDATE(),NEWID(), 'Bimestral', 60);

insert into CalculationTypes (UpdateDate, Id, [Name], NumDays) values (GETUTCDATE(),NEWID(), 'Trimestral', 90);

insert into CalculationTypes (UpdateDate, Id, [Name], NumDays) values (GETUTCDATE(),NEWID(), 'Quatrimestral', 120);

insert into CalculationTypes (UpdateDate, Id, [Name], NumDays) values (GETUTCDATE(),NEWID(), 'Semestral', 180);
 

insert into DaysTypes (UpdateDate, Id, [Name]) values (GETUTCDATE(),NEWID(), 'Laborables');

insert into DaysTypes (UpdateDate, Id, [Name]) values (GETUTCDATE(),NEWID(), 'Naturals');
 

insert into DamagedEquipment (UpdateDate, Id, [Name]) values (GETUTCDATE(),NEWID(), 'Número de Sèrie');

insert into DamagedEquipment (UpdateDate, Id, [Name]) values (GETUTCDATE(),NEWID(), 'Element');

insert into DamagedEquipment (UpdateDate, Id, [Name]) values (GETUTCDATE(),NEWID(), 'Mòdul');
 

GO