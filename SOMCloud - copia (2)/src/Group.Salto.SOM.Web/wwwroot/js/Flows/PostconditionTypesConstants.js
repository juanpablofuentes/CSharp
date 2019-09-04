var app = app || {};
app.postconditionTypes = app.postconditionTypes || {};
app.postconditionTypes.constants = app.postconditionTypes.constants || {};

app.postconditionTypes.constants = (function () {
    var actuationDate = "ActuationDate";
    var actionDate = "ActionDate";
    var actuationEndDate = "ActuationEndDate";
    var billable = "Billable";
    var clientClosureDate = "ClientClosureDate";
    var assignmentDate = "AssignmentDate";
    var internalClosureDate = "InternalClosureDate";
    var resolutionDate = "ResolutionDate";
    var pickupDate = "PickupDate";
    var manipulator = "Manipulator";
    var woObservations = "WoObservations";
    var woReopeningPolicy = "WOReopeningPolicy";
    var typeOtN1 = "TypeOtN1";
    
    return {
        ActuationDate: actuationDate,
        ActionDate: actionDate,
        ActuationEndDate: actuationEndDate,
        Billable: billable,
        ClientClosureDate: clientClosureDate,
        AssignmentDate: assignmentDate,
        InternalClosureDate: internalClosureDate,
        ResolutionDate: resolutionDate,
        PickupDate: pickupDate,
        Manipulator: manipulator,
        WoObservations: woObservations,
        WoReopeningPolicy: woReopeningPolicy,
        TypeOtN1: typeOtN1,
    };
})();