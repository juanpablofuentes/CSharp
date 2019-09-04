var app = app || {};
app.triggerTypes = app.triggerTypes || {};
app.triggerTypes.constants = app.triggerTypes.constants || {};

app.triggerTypes.constants = (function () {
    var woExternalStatus = "WoExternalStatus";
    var stopSlaWatch = "StopSlaWatch";
    var technical = "Technical";
    var create = "Create";
    var woReopening = "WoReopening";
    var queue = "Queue";
    var predefinedServiceId = "PredefinedServiceId";
    var actuationDate = "ActuationDate";
    var woStatus = "WoStatus";
    var restartSlaWatch = "RestartSlaWatch";
    var technicianAndActuationDate = "TechnicianAndActuationDate";
    var noAction = "NoAction";
    var assignmentDate = "AssignmentDate";
               
    return {
        AssignmentDate: assignmentDate,
        ActuationDate: actuationDate,
        Create: create,
        NoAction: noAction,
        PredefinedServiceId: predefinedServiceId,
        Queue: queue,
        RestartSlaWatch: restartSlaWatch,
        StopSlaWatch: stopSlaWatch,
        Technical: technical,
        TechnicianAndActuationDate: technicianAndActuationDate,
        WoReopening: woReopening,
        WoExternalStatus: woExternalStatus,
        WoStatus: woStatus
    };
})();