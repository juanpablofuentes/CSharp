USE [SOMSalto]


Declare @DataActuacioId as uniqueidentifier
Declare @DataActuacioName as varchar(15)
Declare @DataAssignacioId as uniqueidentifier
Declare @DataAssignacioName as varchar(15)
Declare @DataRecullidaInternaId as uniqueidentifier
Declare @DataRecullidaInternaName as varchar(15)
Declare @DataCreacioId as uniqueidentifier
Declare @DataCreacioName as varchar(15)
--Declare @DataAssignacio as uni
--

set @DataActuacioId = 'DF0E6BDA-0AD8-43AD-9142-3C2745F48076';
set @DataActuacioName = 'DataActuacio'
set @DataAssignacioId = '0508956F-98FC-4869-9BB1-888D02464C94';
set @DataAssignacioName = 'DataAssignacio'
set @DataRecullidaInternaId = '9DAC5851-9F92-48CB-A1DE-92FEBE212041';
set @DataRecullidaInternaName = 'DataRecullidaInterna'
set @DataCreacioId = '5CF86BD4-9950-4C47-9F1C-AA2D0BFE141E';
set @DataCreacioName = 'DataCreacio'

update SLA set ReferenceMinutesResponse_New = @DataActuacioId 
 from SLA where ReferenceMinutesResponse = @DataActuacioName 
 update SLA set ReferenceMinutesResponse_New = @DataAssignacioId 
 from SLA where ReferenceMinutesResponse = @DataAssignacioName 
 update SLA set ReferenceMinutesResponse_New = @DataRecullidaInternaId 
 from SLA where ReferenceMinutesResponse = @DataRecullidaInternaName 
 update SLA set ReferenceMinutesResponse_New = @DataCreacioId 
 from SLA where ReferenceMinutesResponse = @DataCreacioName 
 

update SLA set ReferenceMinutesResolution_New = @DataActuacioId 
 from SLA where ReferenceMinutesResolution = @DataActuacioName 
 update SLA set ReferenceMinutesResolution_New = @DataAssignacioId 
 from SLA where ReferenceMinutesResolution = @DataAssignacioName 
 update SLA set ReferenceMinutesResolution_New = @DataRecullidaInternaId 
 from SLA where ReferenceMinutesResolution = @DataRecullidaInternaName 
 update SLA set ReferenceMinutesResolution_New = @DataCreacioId 
 from SLA where ReferenceMinutesResolution = @DataCreacioName 

update SLA set ReferenceMinutesPenaltyUnanswered_New = @DataActuacioId 
from SLA where ReferenceMinutesPenaltyUnanswered = @DataActuacioName 
update SLA set ReferenceMinutesPenaltyUnanswered_New = @DataAssignacioId 
from SLA where ReferenceMinutesPenaltyUnanswered = @DataAssignacioName 
update SLA set ReferenceMinutesPenaltyUnanswered_New = @DataRecullidaInternaId 
from SLA where ReferenceMinutesPenaltyUnanswered = @DataRecullidaInternaName 
update SLA set ReferenceMinutesPenaltyUnanswered_New = @DataCreacioId 
from SLA where ReferenceMinutesPenaltyUnanswered = @DataCreacioName


 update SLA set ReferenceMinutesPenaltyWithoutResolution_New = @DataActuacioId 
from SLA where ReferenceMinutesPenaltyWithoutResolution = @DataActuacioName 
update SLA set ReferenceMinutesPenaltyWithoutResolution_New = @DataAssignacioId 
from SLA where ReferenceMinutesPenaltyWithoutResolution = @DataAssignacioName 
update SLA set ReferenceMinutesPenaltyWithoutResolution_New = @DataRecullidaInternaId 
from SLA where ReferenceMinutesPenaltyWithoutResolution = @DataRecullidaInternaName 
update SLA set ReferenceMinutesPenaltyWithoutResolution_New = @DataCreacioId 
from SLA where ReferenceMinutesPenaltyWithoutResolution = @DataCreacioName 