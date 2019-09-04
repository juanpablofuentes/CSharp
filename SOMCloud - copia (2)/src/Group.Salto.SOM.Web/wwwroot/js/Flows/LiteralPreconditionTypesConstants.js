var app = app || {};
app.literalPreconditionTypes = app.literalPreconditionTypes || {};
app.literalPreconditionTypes.constants = app.literalPreconditionTypes.constants || {};

app.literalPreconditionTypes.constants = (function () {
    var billable = "Billable";
    var createDate = "CreateDate";
    var assignmentDate = "AssignmentDate";
    var collectionDate = "CollectionDate";
    var actuationDate = "ActuationDate";
    var saltoClosureDate = "SaltoClosureDate";
    var clientClosureDate = "ClientClosureDate";
    var wOClientClosureDate = "WOClientClosureDate";
    var wOReopeningPolicy = "WOReopeningPolicy";
    var manipulator = "Manipulator";
    var greaterThan = "MajorQue";
    var lowerThan = "MenorQue";
        
    return {
        Billable: billable,
        AssignmentDate: assignmentDate,
        CreateDate: createDate,
        CollectionDate: collectionDate,
        AssignmentDate: assignmentDate,
        ActuationDate: actuationDate,
        SaltoClosureDate: saltoClosureDate,
        ClientClosureDate: clientClosureDate,
        WOClientClosureDate: wOClientClosureDate,
        WOReopeningPolicy: wOReopeningPolicy,
        Manipulator: manipulator,
        GreaterThan: greaterThan,
        LowerThan: lowerThan
    };
})();