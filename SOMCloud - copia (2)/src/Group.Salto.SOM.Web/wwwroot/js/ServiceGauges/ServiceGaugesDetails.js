var app = app || {};
app.ServiceGauges = app.ServiceGauges || {};
app.ServiceGauges.detail = app.ServiceGauges.detail || {};

app.ServiceGauges.detail = (function () {

    var selectedClient;

    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["StartDate", "EndDate"]);
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, false), "%Y/%m/%d");
        datePicker.hideTime();

        var StartEnd = date(cultureInfo);
        var startDate = StartEnd[0];
        var endDate = StartEnd[1];
        $('#StartDate').val(startDate);
        $('#EndDate').val(endDate);
    };

    var init = function (options) {
        start(options);
        initializeClientAutocomplete(options.Client);
        initializeProjectAutocomplete(options.Project);
        initializeCategoryAutocomplete(options.WoCategory);
        filter(options);
        filterreset(options);
        excel()
    };

    function filter(options) {
        $('#filter').on('click', function () {
            showSpinner();
            var filtres = null;
            var wo = $('#Filter_WoId').val();
            var cultureInfo = getCookie("culture-code").toLowerCase();
            var startDate = cultureInfo !== "en" ? (moment($('#StartDate').val(), "DD/MM/YYYY").format("YYYY/MM/DD")) : (moment($('#StartDate').val()).format("YYYY/MM/DD"));
            var endDate = cultureInfo !== "en" ? (moment($('#EndDate').val(), "DD/MM/YYYY").format("YYYY/MM/DD")) : (moment($('#EndDate').val()).format("YYYY/MM/DD"));
            startDate = moment(startDate); 
            endDate = moment(endDate);
            
            var client = selectedClient.GetSelectedOption() !== undefined ? selectedClient.GetSelectedOption().key : 0;
            var project = selectedProject.GetSelectedOption() !== undefined ? selectedProject.GetSelectedOption().key : 0;
            var WoCategory = selectedWoCategory.GetSelectedOption() !== undefined ? selectedWoCategory.GetSelectedOption().key : 0;
            var projectStr = selectedProject.GetSelectedOption() !== undefined ? selectedProject.GetSelectedOption().value : "All";
            var categoryStr = selectedWoCategory.GetSelectedOption() !== undefined ? selectedWoCategory.GetSelectedOption().value : "All";
            
            var diffTime = endDate.diff( startDate,'days');

            if (diffTime >= 366) { $('#EndDate').val('Max 1 year'); $('#StartDate').val('Max 1 year'); };
            if (diffTime < 366) {
                filtres = { WoId: wo, StartDate: startDate._i, EndDate: endDate._i, Clientint: client, Projectint: project, WoCategory: WoCategory };
                $.post({
                    url: app.config.Urls.getGaugesData,
                    data: filtres,
                    success: function (Result) {
                        charts(Result, options, projectStr, startDate._i, endDate._i);
                    }
                });
                $.post({
                    url: app.config.Urls.getGaugesEconomic,
                    data: filtres,
                    success: function (economic) {
                        EconomicList(economic, projectStr, categoryStr, startDate._i, endDate._i);
                    }
                });
            };
            
            hideSpinner();
        });
    }

    function filterreset(options) {
        $('#filterclear').on('click', function () {
            showSpinner();
            var cultureInfo = getCookie("culture-code").toLowerCase();
            var filtres = null;
            var wo = null; $('#Filter_WoId').val("");
            var StartEnd = date(cultureInfo);
            var startDate = StartEnd[0];
            var endDate = StartEnd[1];
            $('#StartDate').val(startDate);
            $('#EndDate').val(endDate);

            var cultureInfo = getCookie("culture-code").toLowerCase();
            var startDate = cultureInfo !== "en" ? (moment(startDate, "DD/MM/YYYY").format("YYYY/MM/DD")) : (moment(startDate).format("YYYY/MM/DD"));
            var endDate = cultureInfo !== "en" ? (moment(endDate, "DD/MM/YYYY").format("YYYY/MM/DD")) : (moment(endDate).format("YYYY/MM/DD"));
            startDate = moment(startDate);
            endDate = moment(endDate);
            
            var client = 0; selectedClient.Clean();
            var project = 0; selectedProject.Clean();
            var WoCategory = 0; selectedWoCategory.Clean();
            var projectStr = "All";
            var categoryStr = "All";
            filtres = { WoId: wo, StartDate: startDate._i, EndDate: endDate._i, Clientint: client, Projectint: project, WoCategory: WoCategory };
            $.post({
                url: app.config.Urls.getGaugesData,
                data: filtres,
                success: function (Result) {
                    charts(Result, options, projectStr, startDate._i, endDate._i);
                }
            });
            $.post({
                url: app.config.Urls.getGaugesEconomic,
                data: filtres,
                success: function (economic) {
                    EconomicList(economic, projectStr, categoryStr, startDate._i, endDate._i);
                }
            });
            hideSpinner();
        });
    }

    function start(options) {
        showSpinner();
        calendarLoad();
        var filtres = null;
        var wo = $('#Filter_WoId').val();
        var startDate = $('#StartDate').val();
        var endDate = $('#EndDate').val();

        var cultureInfo = getCookie("culture-code").toLowerCase().toString();
        var startDate = cultureInfo !== "en" ? (moment(startDate, "DD/MM/YYYY").format("YYYY/MM/DD")) : (moment(startDate).format("YYYY/MM/DD"));
        var endDate = cultureInfo !== "en" ? (moment(endDate, "DD/MM/YYYY").format("YYYY/MM/DD")) : (moment(endDate).format("YYYY/MM/DD"));
        startDate = moment(startDate);
        endDate = moment(endDate);

        var client = 0;
        var project = 0;
        var WoCategory = 0;
        var projectStr = "All";
        var categoryStr = "All";
        filtres = { WoId: wo, StartDate: startDate._i, EndDate: endDate._i, Clientint: client, Projectint: project, WoCategory: WoCategory };
        $.post({
            url: app.config.Urls.getGaugesData,
            data: filtres,
            success: function (Result) {
                charts(Result, options, projectStr, startDate._i, endDate._i);
            }
        });
        $.post({
            url: app.config.Urls.getGaugesEconomic,
            data: filtres,
            success: function (Economic) {
                EconomicList(Economic, projectStr, categoryStr, startDate._i, endDate._i);
            }
        });
        hideSpinner();
    }

    function excel() {
        $('#Export').on('click', function () {
            $('#ReportTable').table2excel({ name: "ValoresEconomicos", filename: "ValoresEconomicos" });
        });
    }

    function charts(Result, options, projectStr, startDate, endDate) {
        
        var total = Result.totalOts;
        createVisitGauge(Result.averageVisits, Result.maxVisits, Object.keys(Result.intervalVisits)[0], Object.values(Result.intervalVisits)[0], options.name, options.Name, options.Name);
        createVisitGauge(Result.averageKilometers, Result.maxKilometers, Object.keys(Result.intervalKilometers)[0], Object.values(Result.intervalKilometers)[0], options.name2, options.Name2, options.units1);
        createVisitGauge(Result.averageWaitTime, Result.maxWaitTime, Object.keys(Result.intervalWaitTime)[0], Object.values(Result.intervalWaitTime)[0], options.name3, options.Name3, options.units2);
        createVisitGauge(Result.averageOnSiteTime, Result.maxOnSiteTime, Object.keys(Result.intervalOnSite)[0], Object.values(Result.intervalOnSite)[0], options.name4, options.Name4, options.units2);
        createVisitGauge(Result.averageTravelTime, Result.maxTravelTime, Object.keys(Result.intervalTravel)[0], Object.values(Result.intervalTravel)[0], options.name5, options.Name5, options.units2);
        createLinesGauge(1, Result.getMonthlyProjectCostList, options.name6, options.Name6, projectStr, startDate, endDate);
        createLinesGauge(1, Result.getMonthlyProjectRevenueList, options.name7, options.Name7, projectStr, startDate, endDate);
        createLinesGauge(1, Result.getMonthlyProjectMarginList, options.name8, options.Name8, projectStr, startDate, endDate);
        drawPiChar2data(Result.responseSla, Result.totalSla, options.name9, options.Name9);
        drawPiChar2data(Result.resolutionSla, Result.totalSla, options.name10, options.Name10);
        $('#total').text(total);
        $('#total3').text(Result.totalClosedToday);
        $('#total4').text(Result.totalOpenedToday);
        $('#stre1').text(Object.keys(Result.assets[0])[0]);
        $('#str2').text(Object.keys(Result.assets[1])[0]);
        $('#str3').text(Object.keys(Result.assets[2])[0]);
        $('#av1').text(Object.values(Result.assets[0])[0]);
        $('#av2').text(Object.values(Result.assets[1])[0]);
        $('#av3').text(Object.values(Result.assets[2])[0]);
    }
    function EconomicList(economic, projectStr, categoryStr, startDate, endDate) {
        var timeH = (economic.hoursDirectWorkForce > 0) ? (economic.hoursDirectWorkForce / 6) : 0;
        var timeM = (economic.hoursDirectWorkForce > 0) ? (economic.hoursDirectWorkForce % 6) : 0;
        var cost = (economic.expensesTravel + economic.expensesKm + economic.expensesOther + economic.costMaterials + economic.costDirectWorkForce + economic.costMaterials);
        var totalIncomes = (economic.revenueWorkForce + economic.revenueMaterials);
        var MarginE = totalIncomes - cost;
        var MarginP = MarginE * 100 / (cost == 0 ? 1 : cost);

        document.getElementById("RevenueWorkForce").innerHTML = economic.revenueWorkForce + "€";
        document.getElementById("RevenueMaterials").innerHTML = economic.revenueMaterials + "€";
        document.getElementById("Incomes").innerHTML = "<b>" + totalIncomes + "€ </b>";
        document.getElementById("CostDirectWorkForce").innerHTML = economic.costDirectWorkForce + "€";
        document.getElementById("HoursDirectWorkForce").innerHTML = timeH + " H " + timeM + " m " ;
        document.getElementById("CostMaterials").innerHTML = economic.costMaterials + "€";
        document.getElementById("CostOutSource").innerHTML = economic.costOutSource + "€";
        document.getElementById("ExpensesOther").innerHTML = economic.expensesOther + "€";
        document.getElementById("ExpensesTravel").innerHTML = economic.expensesTravel + "€";
        document.getElementById("ExpensesWait").innerHTML = economic.expensesWait + "€";
        document.getElementById("ExpensesKm").innerHTML = economic.expensesKm + "€";
        document.getElementById("TotalCosts").innerHTML = "<b>" + cost + "€ </b>";
        document.getElementById("Margin").innerHTML = "<b>" + MarginE + "€ </b>";
        document.getElementById("MarginP").innerHTML = "<b>" + MarginP + "% </b>";

        document.getElementById("Project").innerHTML = projectStr ;
        document.getElementById("Category").innerHTML = categoryStr ;
        document.getElementById("StartDateP").innerHTML = startDate ;
        document.getElementById("EndDateP").innerHTML = endDate ;
    }

    function date(dateTimeFormat) {
        var year = new Date().getFullYear().toString();
        var day = new Date().getDate();
        var numdays = new Date().getDay();
        var month = new Date().getMonth() + 1;
        var day1; var month1; var year1;
        var day2; var month2; var year2;
        var last_acmh = new Date(year, month, 0);
        var varlastDate_acmh = last_acmh.getDate();
        var lastday_acmh = last_acmh.getDay();
        var last_pasmh = new Date(year, month - 1, 0);
        var lastDate_pasmh = last_pasmh.getDate();
        var lastday_pasmh = last_pasmh.getDay();
        day1 = day - (numdays - 1);
        year1 = year;
        month1 = month;
        if (day1 < 1) {
            day1 = lastDate_pasmh - ((numdays - 1) - day);
            month1 = month - 1;
            if (month1 < 1) {
                month1 = 12;
                year1 = year - 1;
            }
        }
        day2 = day + (7 - numdays);
        year2 = year;
        month2 = month;
        if (day2 > 30) {
            day2 = (7 - numdays) - (lastday_acmh - numdays);
            month2 = month + 1;
            if (month2 > 12) {
                month2 = 1;
                year2 = year + 1;
            }
        }
        month1 = month1 < 10 ? "0" + month1 : month1;
        day1 = day1 < 10 ? "0" + day1 : day1;
        month2 = month2 < 10 ? "0" + month2 : month2;
        day2 = day2 < 10 ? "0" + day2 : day2;
        var startDate = dateTimeFormat !== "en" ? day1 + "/" + month1 + "/" + year1 : month1 + "." + day1 + "." + year1;
        var endDate = dateTimeFormat !== "en" ? day2 + "/" + month2 + "/" + year2 : month2 + "." + day2 + "." + year2;

        return [startDate, endDate];
    }

    function initializeClientAutocomplete(options) {
        var ClientCombo = new autocomplete();
        ClientCombo.Init("#ClientContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.getClient,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 0,
                nColumns: false,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        selectedClient = ClientCombo;
        return ClientCombo;
    }

    function getProjectFilter(text) {
        var selectedOption = selectedClient.GetSelectedOption();
        var data = {
            Text: text,
            Selected: selectedOption !== undefined ? selectedOption.key : []
        };
        return data;
    }

    function initializeProjectAutocomplete(options) {
        var ProjectCombo = new autocomplete();
        ProjectCombo.Init("#ProjectContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.GetProjectAll,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getProjectFilter,
                ajaxMethodType: app.constants.Post
            });
        selectedProject = ProjectCombo;
        return ProjectCombo;
    }

    function getCategorytFilter(text) {
        var selectedOption = selectedProject.GetSelectedOption();
        var data = {
            Text: text,
            Selected: selectedOption !== undefined ? selectedOption.key : []
        };
        return data;
    }

    function initializeCategoryAutocomplete(options) {
        var WoCategoryCombo = new autocomplete();
        WoCategoryCombo.Init("#CategoryContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.GetWoCategoryAll,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getCategorytFilter,
                ajaxMethodType: app.constants.Post
            });
        selectedWoCategory = WoCategoryCombo;
        return WoCategoryCombo;
    }

    return {
        Init: init,
        CalendarLoad: calendarLoad,
        Start: start
    };
})();