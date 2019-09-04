var app = app || {};
app.FlowWorkOrderDerivate = app.FlowWorkOrderDerivate || {};

app.FlowWorkOrderDerivate = (function (event) {
    var options;
    var template = '<div class="row workorderderived-content mb-3" id="wo-[woId]">' +
        '<div class="col-11 workorderderived">' +
        '<div class="row item-workorderderived">' +
        '<div class="col-12 p-0">' +
        '<div class="form-group row pt-2 pb-2 m-0">' +
        '<div class="col p-0">' +
        '<div>' +
        '<div>' +
        '[rows]' +
        '</div>' +
        '</div>' +
        '<div class="collaps-icons float-right d-flex align-items-center">' +
        '<a href="#" class="mr-2" onclick="app.FlowWorkOrderDerivate.EditWorkOrderDerivated([woId2])"><i class="fa fa-pencil"></i></a>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>';

    var templateAdd =
        '<div class="row workorderderived-row-add">'+
        '<div class="col-12 p-0">' +
        '<div class="collaps-icons text-right">' +
        '<a id="add" href="#" class="mr-2" onclick="app.FlowWorkOrderDerivate.CreateNewWorkOrderDerivated([taskid])"><i class="fa fa-plus fa-lg"></i></a>' +
        '</div>' +
        '</div>' +
        '</div>';

    var init = function (_options) {
        options = _options;
    };

    var createNewWorkOrderDerivated = function (taskid)
    {
        var flowid = $("#FlowId").val();
        var url = "/workorder/CreateDerived";
        document.location.href = url + "?taskId=" + taskid + "&flowId=" + flowid;
    }

    var editWorkOrderDerivated = function (id) {
        
        var flowid = $("#FlowId").val();
        var url = "/workorder/EditDerived";
        document.location.href = url + "?id=" + id + "&flowId=" + flowid;
    }

    var loadData = function (e) {
        showSpinner();
        var id = e.currentTarget.attributes["data-taskid"].value;
        var url = app.config.Urls.getWorkOrderDerivatedByTaskId;
        var workorderderivated = apiCall(url, 'GET', 'json', { id: id });
        workorderderivated.done(function (res) {
            var result = '';
            var _templateAdd = templateAdd.replace("[taskid]", id);
            var woid = 0;
            for (var i = 0; i < res.length; i++) {
                woid = res[i][0].id;
                var _template = template;
                _template = _template.replace("[woId]", woid);
                _template = _template.replace("[woId2]", woid);
                var row = '';
                for (var r = 1; r < res[i].length; r++) {
                    row += '<strong class="font-weight-bold">' + res[i][r].name + ':</strong>' + res[i][r].id + '<br>';
                }
                _template = _template.replace("[rows]", row);
                result += _template;
            }
            result += _templateAdd;
            $("#container-workorderderived-" + id).html(result);
        }).always(function (err) {
            hideSpinner();
        });
    }
    
    return {
        Init: init,
        CreateNewWorkOrderDerivated: createNewWorkOrderDerivated,
        EditWorkOrderDerivated: editWorkOrderDerivated,
        LoadData: loadData
    };
})();