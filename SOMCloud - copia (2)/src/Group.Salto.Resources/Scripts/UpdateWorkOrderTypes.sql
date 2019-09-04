BEGIN TRAN UPDATEWorkOrderTypes
declare @MaxValue int = (select max(id) from WorkOrderTypes);
declare @index int = 1;
while(@MaxValue >= @index)
BEGIN
;WITH parent AS
(
    SELECT id, WorkOrderTypesFatherId,CollectionsTypesWorkOrdersId  from WorkOrderTypes WHERE id = @index
    UNION ALL 
    SELECT t.id, t.WorkOrderTypesFatherId, t.CollectionsTypesWorkOrdersId FROM parent
    INNER JOIN WorkOrderTypes t ON t.id =  parent.WorkOrderTypesFatherId
)

Update WorkOrderTypes Set CollectionsTypesWorkOrdersId = (select CollectionsTypesWorkOrdersId from WorkOrderTypes where Id = (SELECT TOP 1 id FROM  parent order by CollectionsTypesWorkOrdersId desc, id asc)) where id = @index

SET @index = @index + 1;
END;

Select * from WorkOrderTypes order by Id
rollback
--commit

