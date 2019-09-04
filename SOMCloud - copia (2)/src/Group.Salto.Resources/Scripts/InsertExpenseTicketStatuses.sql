USE [SOMSalto]
GO

INSERT INTO [dbo].[ExpenseTicketStatuses]([UpdateDate],[Id],[Description])VALUES(GETUTCDATE(),NEWID(),'Paid')
INSERT INTO [dbo].[ExpenseTicketStatuses]([UpdateDate],[Id],[Description])VALUES(GETUTCDATE(),NEWID(),'Accepted')
INSERT INTO [dbo].[ExpenseTicketStatuses]([UpdateDate],[Id],[Description])VALUES(GETUTCDATE(),NEWID(),'Pending')
INSERT INTO [dbo].[ExpenseTicketStatuses]([UpdateDate],[Id],[Description])VALUES(GETUTCDATE(),NEWID(),'Finished')
INSERT INTO [dbo].[ExpenseTicketStatuses]([UpdateDate],[Id],[Description])VALUES(GETUTCDATE(),NEWID(),'Rejected')
INSERT INTO [dbo].[ExpenseTicketStatuses]([UpdateDate],[Id],[Description])VALUES(GETUTCDATE(),NEWID(),'Escaled')

GO
