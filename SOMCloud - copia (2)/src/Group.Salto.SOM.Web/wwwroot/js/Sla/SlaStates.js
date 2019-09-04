app.slastates = (function () {
    var settings;
    var states;
    var id;
    var slaid;
    var finalMinutes;
    var color;
    var editMode = false;
    var tempcolor;
    var tempfinalMinutes;
    var content;
    var rowCreatedCount = 0;

    var createState = function (e) {
        const newState =
            "<tr id="+rowCreatedCount+">"
            + "<div hidden='true'><input type='text' id='StatesSla__" + rowCreatedCount + "__ValueSecondary'  name=\"StatesSla[" + rowCreatedCount + "].ValueSecondary\" value='" + slaid +"' class='form-control' readonly /></div>"
            + "<td>"
            + "<div id='colorPickerContainer' hidden><input type='text' id='StatesSla__" + rowCreatedCount + "__Text'  name=\"StatesSla[" + rowCreatedCount + "].Text\" value='" + color +"' class='form-control' readonly /> <div id='colorPicker'></div></div>"         
            + "<div class=\"col-calendar-color text-center\">"
            + "<span class=\"calendar-color\" style=\"background-color:" + color + "\" ></span>"
            + "</div>"
            + "</td>"
            + "<td>"
            + "<input type='hidden' class='form-control' id='StatesSla__" + rowCreatedCount + "__TextSecondary' name=\"StatesSla[" + rowCreatedCount + "].TextSecondary\" value='" + finalMinutes + "'/> "
            + "<p id = '" + rowCreatedCount + "' class=\"text-center\">" + finalMinutes + "</p>"
            +"</td> "
            + "<td>"            
            + "<a onclick=\"javascript:app.slastates.EditState(this," + rowCreatedCount + ",'" + color + "'," + finalMinutes + ")\" href='#' class='editButton'>"
            + "<i class=\"fa fa-pencil fa-lg\"></i>"
            + "</a>"
            + "<a onclick=\"javascript:app.slastates.DeleteState(this)\" href='#' class='deleteButton'>"
            + "<i class=\"fa fa-trash-o fa-lg\"></i>"
            + "</a>"
            + "</td>"
            + "</tr>";
        return newState;
    }

    var createdState = function (e) {
        const newState =
            "<tr id="+e+">"
            + "<div hidden='true'><input type='text' id='StatesSla__" + e + "__Value' name=\"StatesSla[" + e + "].Value\" value='" + id + "' class='form-control' readonly />"
            + "</div>"
            + "<div hidden='true'><input type='text' id='StatesSla__" + e + "__ValueSecondary' name=\"StatesSla[" + e + "].ValueSecondary\" value='" + slaid + "' class='form-control' readonly />"
            + "</div>"
            + "<td>"
            + "<div id='colorPickerContainer' hidden><input type='text' id='StatesSla__" + e + "__Text' name=\"StatesSla[" + e + "].Text\" value='" + color + "' class='form-control' readonly />"
            + "<div id='colorPicker'></div>"
            + "</div >"
            + "<div class=\"col-calendar-color text-center\">"
            + "<span class=\"calendar-color\" style=\"background-color:" + color + "\" ></span>"
            + "</div>"
            + "</td>"
            + "<td>"
            + "<input type='hidden' class='form-control' id='StatesSla__" + e + "__TextSecondary' name=\"StatesSla[" + e + "].TextSecondary\" value = '" + finalMinutes + "' /> "
            + "<p id = '" + e + "' class=\"text-center\">" + finalMinutes + "</p>"
            + "</td > "
            + "<td>"
            + "<a id='StatesSla__" + e + "__TextSecondary' onclick=\"javascript:app.slastates.EditState(this," + e + ",'" + color + "'," + finalMinutes + ")\" href='#' class='editButton'>"
            + "<i class=\"fa fa-pencil fa-lg\"></i>"
            + "</a>"
            + "<a onclick=\"javascript:app.slastates.DeleteState(this," + e +")\" href='#' class='deleteButton'>"
            + "<i class=\"fa fa-trash-o fa-lg\"></i>"
            + "</a>"
            + "</td>"
            + "</tr>";
        return newState;
    }

    var editState = function (ids) {

        const editState =
            "<tr id="+ids+">"
            + "<div hidden='true'><input type='text' id='StatesSla__" +ids + "__ValueSecondary' name=\"StatesSla[" + ids + "].ValueSecondary\" value='" + slaid + "' class='form-control' readonly />"
            + "</div>"
            + "<td>"
            + "<div id=\"colorPickerContainer\">"
            + "<input type=\"text\" id=\"colorPickerInput\" class=\"form-control\" value=" + color + " readonly placeholder=\"Select color...\" />"
            + "<div id=\"colorPicker\"></div>"
            + "</div>"
            + "</td>"
            + "<td><input type=\"number\" id=\"SubMenuContent_NewEstatTemps\"  class=\"form - control\" placeholder=\'" + finalMinutes + "'>"
            + "<div class=\"input-group-append\" >"
            + "</div ></td>"
            + "<td>"
            + "<a onclick=\"javascript:app.slastates.ConfirmEditState(this,"+ ids +")\" href='#' class='editButton'>"
            + "<i class=\"fa fa-check-circle fa-lg\"></i>"
            + "</a>"
            + "<a onclick=\"javascript:app.slastates.CancelEditState(this,"+ids+")\" href='#' class='deleteButton'>"
            + "<i class=\"fa fa-times-circle fa-lg\"></i>"
            + "</a>"
            + "</td>"
            + "</tr>";
        return editState;
    }
   
    function getEditFieldsData() {
        
        finalMinutes = document.getElementById("SubMenuContent_NewEstatTemps").value;
        color = document.getElementById("colorPickerInput").value;
        
    }

    function getFieldsData(id) {
        slaid = id;
        finalMinutes = "";
        color = "";
        finalMinutes = document.getElementById("SubMenuContent_NewEstatTemps").value;
        color = document.getElementById("colorPickerInput").value;
        
    }

    var loadPage = function (e) {
        content = document.getElementById("SubMenuContent_EstatsDataList").getElementsByTagName("tbody")[0]
    }

    var addhtmlstate = function (e) {
        var row = content.insertRow(rowCreatedCount);
        content.childNodes[rowCreatedCount].id = rowCreatedCount;
        row.innerHTML = e;
        rowCreatedCount += 1;
    }

    var edithtmlstate = function (e, row,id,co,fminutes) {
        var i = row.parentNode.parentNode;
        //tempcolor = i.childNodes[2].childNodes[0].childNodes[0].value;
        //tempfinalMinutes = i.childNodes[3].childNodes[0].value;
        i.innerHTML = e;
        editMode = true;
        var color = i.childNodes[1];
        var myColorPicker = new dhtmlXColorPicker({            
            color: color.childNodes[0].value,
            skin: app.calendar.constants.ColorPickerSkin            
        });
        myColorPicker.linkTo(color.childNodes[0].childNodes[0].id);
    }

    function deletehtmlstate(row,id) {
        var i = row.parentNode.parentNode;
        i.parentNode.removeChild(i);
        rowCreatedCount -= 1;
        updateIdsRow();
        i.id = rowCreatedCount;
        return i;
    }

    function updateIdsRow(){
        loadPage();
        for (var item = 0; item < content.children.length; item++) {
            content.childNodes[item].id = item;
            content.childNodes[item].childNodes[0].childNodes[0].setAttribute("id", "StatesSla__" + item + "__Value");
            content.childNodes[item].childNodes[0].childNodes[0].setAttribute("name", "StatesSla[" + item + "].Value");
            content.childNodes[item].childNodes[1].childNodes[0].setAttribute("id", "StatesSla__" + item + "__ValueSecondary");
            content.childNodes[item].childNodes[1].childNodes[0].setAttribute("name", "StatesSla[" + item + "].ValueSecondary");
            content.childNodes[item].childNodes[2].childNodes[0].childNodes[0].setAttribute("id", "StatesSla__" + item + "__Text");
            content.childNodes[item].childNodes[2].childNodes[0].childNodes[0].setAttribute("name", "StatesSla[" + item + "].Text");
            content.childNodes[item].childNodes[3].childNodes[0].setAttribute("id", "StatesSla__" + item + "__TextSecondary");
            content.childNodes[item].childNodes[3].childNodes[0].setAttribute("name", "StatesSla[" + item + "].TextSecondary");
            content.childNodes[item].childNodes[5].childNodes[0].setAttribute("id", "StatesSla__" + item + "__TextSecondary");
            content.childNodes[item].childNodes[5].childNodes[0].setAttribute("onclick", "javascript:app.slastates.EditState(this," + item +")");
            content.childNodes[item].childNodes[5].childNodes[1].setAttribute("onclick", "javascript:app.slastates.DeleteState(this," + item +")");
        }
    }
    
    var addstate = function (id) {
        
        getFieldsData(id);
        if (finalMinutes != "") {
        loadPage();
        
            var state = createState();
            addhtmlstate(state);
        }
    }

    var deletestate = function (row,id) {
        deletehtmlstate(row,id);
    }

    var editstate = function (row, idsa,c,fmin) {
        if (!editMode) {
            color = c;
            finalMinutes = fmin;
            var state = editState(idsa);
            edithtmlstate(state, row, idsa,c,fmin);
        }
    }

    var confirmeditstate = function (e,id) {
        getEditFieldsData();
        editMode = false;
        var state = createdState(id);
        confirmhtmlstate(state,id);
    }

    var confirmhtmlstate = function (s, id) {
        content.childNodes[id].id = id;
        for (var item = 0; item < content.childNodes.length; item++) {
            var child = content.childNodes;
            if (child[item].id == id) {
                content.childNodes[id].innerHTML = s;
            }
        } 
    }

    var cancelhtmlstate = function (e,ids2) {
        content.childNodes[ids2].id = ids2;
        for (var item = 0; item < content.childNodes.length; item++) {
            var child = content.childNodes;
            if (child[item].id == ids2) {
                content.childNodes[ids2].innerHTML = e;
            }
        } 
    }

    var canceleditstate = function (row, ids) {
        var state = createdState(ids);
        editMode = false;
        cancelhtmlstate(state,ids);
    }

    var loadstates = function (options) {
        settings = $.extend({}, options);
        states = settings.states;
        loadPage();
        for (var i = 0; i < states.length; i++) {
            slaid = states[i].Value;
            id = states[i].ValueSecondary;
            color = states[i].Text;
            finalMinutes = states[i].TextSecondary;
            var state = createdState(i);
            addhtmlstate(state);
        }
    }

    var getstatesuptated = function () {
        return states;
    }
    
    return {
        LoadStates: loadstates,
        AddState: addstate,
        GetStatesUptated: getstatesuptated, 
        EditState:editstate,
        DeleteState: deletestate,
        ConfirmEditState:confirmeditstate,
        CancelEditState: canceleditstate
    }
})();