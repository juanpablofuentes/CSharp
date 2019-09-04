USE SOM
BEGIN TRANSACTION TRANS

--insert id directly so migration works flawlessly
SET IDENTITY_INSERT ItemTypes ON
INSERT ItemTypes(UpdateDate,Id,Name)VALUES (GETDATE(),0,'Product')
INSERT ItemTypes(UpdateDate,Id,Name)VALUES (GETDATE(),1,'Workforce')
INSERT ItemTypes(UpdateDate,Id,Name)VALUES (GETDATE(),2,'Consumable')
SET IDENTITY_INSERT IdentityTable OFF

COMMIT TRANSACTION TRANS