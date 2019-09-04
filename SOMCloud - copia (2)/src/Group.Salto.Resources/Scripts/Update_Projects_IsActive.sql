UPDATE [dbo].[Projects]
   SET [IsActive] = 1
 WHERE [Status] = 'ACTIU'

UPDATE [dbo].[Projects]
   SET [IsActive] = 0
 WHERE [Status] = 'INACTIU'