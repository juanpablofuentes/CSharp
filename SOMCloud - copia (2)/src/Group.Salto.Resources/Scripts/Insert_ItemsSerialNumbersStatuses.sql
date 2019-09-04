SET IDENTITY_INSERT ItemsSerialNumberStatuses ON
GO
INSERT INTO ItemsSerialNumberStatuses (UpdateDate, Id, Name) VALUES (GETDATE(),1, 'ACTIVE')
INSERT INTO ItemsSerialNumberStatuses (UpdateDate, Id, Name) VALUES (GETDATE(),2, 'BLOCKED')
INSERT INTO ItemsSerialNumberStatuses (UpdateDate, Id, Name) VALUES (GETDATE(),3, 'REJECTED')
GO
SET IDENTITY_INSERT ItemsSerialNumberStatuses OFF
GO