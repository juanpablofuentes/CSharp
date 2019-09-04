BEGIN TRAN UPDATEClosingCodes
declare @MaxValue int = (select max(id) from ClosingCodes);
while(@MaxValue > 0)
BEGIN
;WITH parent AS
(
    SELECT id, ClosingCodesFatherId  from ClosingCodes WHERE id = @MaxValue
    UNION ALL 
    SELECT t.id, t.ClosingCodesFatherId FROM parent
    INNER JOIN ClosingCodes t ON t.id =  parent.ClosingCodesFatherId
)
Update ClosingCodes Set CollectionsClosureCodesId = (select CollectionsClosureCodesId from ClosingCodes where Id = (SELECT TOP 1 id FROM  parent order by id asc)) where id = @MaxValue

SET @MaxValue = @MaxValue - 1;
END;

Select * from ClosingCodes
rollback
--commit

