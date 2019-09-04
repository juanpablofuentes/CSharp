--DROP INDEX IX_WorkOrders_ForWorkOrderList ON dbo.WorkOrders
--GO
CREATE NONCLUSTERED INDEX IX_WorkOrders_ForWorkOrderList ON dbo.WorkOrders
(
	QueueId asc
)
INCLUDE (ProjectId, WorkOrderCategoryId, CreationDate, ActionDate, LocationId, PeopleResponsibleId, InternalIdentifier, ExternalIdentifier, FinalClientId, WorkOrderTypesId, PeopleManipulatorId, AssetId, AssignmentTime, PickUpTime, FinalClientClosingTime, InternalClosingTime) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = ON) ON [PRIMARY]
GO

--DROP INDEX IX_Locations_SearchWorkOrder ON dbo.WorkOrders
--GO
CREATE NONCLUSTERED INDEX IX_Locations_SearchWorkOrder ON dbo.Locations
(
	[Name] ASC
)
INCLUDE (Code,	Phone1,	Phone2,	Phone3) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

--DROP INDEX IX_WorkOrderAnalysis_ForWorkOrderList
--GO
CREATE NONCLUSTERED INDEX IX_WorkOrderAnalysis_ForWorkOrderList ON dbo.WorkOrderAnalysis
(
	WorkOrderCode ASC
)
INCLUDE (TotalWorkedTime, OnSiteTime, TravelTime, Kilometers, NumberOfVisitsToClient, NumberOfIntervention, MeetResolutionSLA, MeetResponseSLA, ClosingWODate) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

ALTER INDEX ALL ON dbo.WorkOrders REORGANIZE;   
ALTER INDEX ALL ON dbo.WorkOrderAnalysis REORGANIZE;   
ALTER INDEX ALL ON dbo.locations REORGANIZE;
ALTER INDEX ALL ON dbo.People REORGANIZE
ALTER INDEX ALL ON dbo.ExternalWorOrderStatuses REORGANIZE
ALTER INDEX ALL ON dbo.Locations REORGANIZE
ALTER INDEX ALL ON dbo.Projects REORGANIZE
ALTER INDEX ALL ON dbo.Queues REORGANIZE
ALTER INDEX ALL ON dbo.WorkOrderTypes REORGANIZE
ALTER INDEX ALL ON dbo.WorkOrderStatuses REORGANIZE
ALTER INDEX ALL ON WorkOrderStatusesTranslations REORGANIZE
ALTER INDEX ALL ON dbo.WorkOrderCategories REORGANIZE

ALTER INDEX ALL ON dbo.FinalClients Rebuild;   
ALTER INDEX ALL ON dbo.FinalClients REORGANIZE;   

UPDATE STATISTICS dbo.WorkOrders;
UPDATE STATISTICS dbo.WorkOrderAnalysis;
UPDATE STATISTICS dbo.People
UPDATE STATISTICS dbo.ExternalWorOrderStatuses
UPDATE STATISTICS dbo.Locations
UPDATE STATISTICS dbo.Projects 
UPDATE STATISTICS dbo.Queues 
UPDATE STATISTICS dbo.WorkOrderTypes
UPDATE STATISTICS dbo.WorkOrderStatuses 
UPDATE STATISTICS WorkOrderStatusesTranslations
UPDATE STATISTICS dbo.WorkOrderCategories
UPDATE STATISTICS dbo.Locations
UPDATE STATISTICS dbo.FinalClients

ALTER INDEX ALL ON dbo.PeoplePermissions REORGANIZE;   
ALTER INDEX ALL ON dbo.PeoplePermissions REBUILD;   
UPDATE STATISTICS dbo.PeoplePermissions;

ALTER INDEX ALL ON dbo.PermissionsQueues REORGANIZE;   
ALTER INDEX ALL ON dbo.PermissionsQueues REBUILD;   
UPDATE STATISTICS dbo.PermissionsQueues;

ALTER INDEX ALL ON dbo.ProjectsPermissions REORGANIZE;   
ALTER INDEX ALL ON dbo.ProjectsPermissions REBUILD;   
UPDATE STATISTICS dbo.ProjectsPermissions;

ALTER INDEX ALL ON dbo.WorkOrderCategoryPermissions REORGANIZE;   
ALTER INDEX ALL ON dbo.WorkOrderCategoryPermissions REBUILD;   
UPDATE STATISTICS dbo.WorkOrderCategoryPermissions;