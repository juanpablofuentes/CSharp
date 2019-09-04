var app = app || {};
app.MailTemplate = app.MailTemplate || {};
app.MailTemplate.detail = app.MailTemplate.detail || {};

app.MailTemplate.detail = (function () {

    var init = function (options) {
        text()
        //WorkForm(options)
    };

    function toTimeHtml(date) {
        return '<time datetime="' + date.toString() + '">' + date.toDateString() + '</time>';
    }

    function text() {
        tinymce.init({
            selector: 'textarea',  
            menu: {
                file: { title: 'File', items: 'newdocument' },
                edit: { title: 'Edit', items: 'undo redo | cut copy paste pastetext | selectall' },
                insert: { title: 'Insert', items: 'link media | template hr' },
                format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript | formats | removeformat' },
                table: { title: 'Table', items: 'inserttable tableprops deletetable | cell row column' },
                tools: { title: 'Tools', items: 'spellchecker code' },
                woprop: { title: 'WorkOrder', items: 'WoId WoClientId WoClientSiteId Observations Reparation Actuacio FiActuacio Tipologia Estado Usuario Ubicacion Orden' },
                formprop: { title: 'Form', items: 'FormId FormCreationDate CodigosCierre DniPersona NomPersona' }
            },
            menubar: true,
            plugins: 'code link image table',
            toolbar: [
                'undo redo | cut copy paste | styleselect | fontselect fontsizeselect | bold italic underline | bullist numlist | link image | table | alignleft aligncenter alignright alignjustify | code'
            ],
            setup: function (editor) {
                editor.addMenuItem('WoId', {
                    text: 'Id',
                    context: 'woprop',
                    onclick: function () { editor.insertContent("[OT][ID]"); }
                });

                editor.addMenuItem('WoClientId', {
                    text: 'InternalId',
                    context: 'woprop',
                    onclick: function () { editor.insertContent("[OT][OTCLIENT]"); }
                });

                editor.addMenuItem('WoClientSiteId', {
                    text: 'ExternalId',
                    context: 'woprop',
                    onclick: function () { editor.insertContent("[OT][OTCLIENTSITE]"); }
                });

                editor.addMenuItem('WoSiteCode', {
                    text: 'SiteCode',
                    context: 'woprop',
                    onclick: function () { editor.insertContent("[OT][SITECODE]"); }
                });
                editor.addMenuItem('Observations', {
                    text: 'Observations',
                    context: 'woprop',
                    onclick: function () { editor.insertContent("[OT][OBSERVACIONS]"); }
                });

                editor.addMenuItem('Reparation', {
                    text: 'Reparation',
                    context: 'woprop',
                    onclick: function () { editor.insertContent("[OT][REPARACIO]"); }
                });

                editor.addMenuItem('Actuation', {
                    text: 'ActuationDate',
                    context: 'woprop',
                    onclick: function () { editor.insertContent("[OT][ACTUACIO][yyyy-MM-dd hh:mm]"); }
                });

                editor.addMenuItem('ActuationEnd', {
                    text: 'ActuationEndDate',
                    context: 'woprop',
                    onclick: function () { editor.insertContent("[OT][FIACTUACIO][yyyy-MM-dd hh:mm]"); }
                });
                editor.addMenuItem('Usuario', {
                    text: 'User',
                    context: 'woprop',
                    menu: [

                        { text: 'DNI Person', onclick: function () { editor.insertContent("[OT][DNIPERSONA]"); } },
                        { text: 'FullName Person', onclick: function () { editor.insertContent("[OT][NOMPERSONA]"); } }
                        // { text: 'Name Person Task', onclick: function () { editor.insertContent("[FORM][NOMPERSONATASCA]"); } }
                    ]
                });
                editor.addMenuItem('Ubicacion', {
                    text: 'Localization',
                    context: 'woprop',
                    menu: [

                        { text: 'Site', onclick: function () { editor.insertContent("[OT][LOCALIDAD]"); } },
                        { text: 'Province', onclick: function () { editor.insertContent("[OT][PROVINCIA]"); } },
                        { text: 'CodePostal', onclick: function () { editor.insertContent("[OT][CODIGOPOSTAL]"); } },
                        { text: 'Address', onclick: function () { editor.insertContent("[OT][DIRECCION]"); } },
                        { text: 'Zone', onclick: function () { editor.insertContent("[OT][ZONA]"); } },
                        { text: 'Subzone', onclick: function () { editor.insertContent("[OT][SUBZONA]"); } },
                        { text: 'Area', onclick: function () { editor.insertContent("[OT][AREA]"); } },
                        { text: 'Latitude', onclick: function () { editor.insertContent("[OT][LATITUD]"); } },
                        { text: 'Longitude', onclick: function () { editor.insertContent("[OT][LONGITUD]"); } }
                    ]
                });
                editor.addMenuItem('Estado', {
                    text: 'Status',
                    context: 'woprop',
                    menu: [

                        { text: 'Id Status', onclick: function () { editor.insertContent("[OT][IDESTADO]"); } },
                        { text: 'LiteralName Status', onclick: function () { editor.insertContent("[OT][NOMBREESTADO]"); } },
                        { text: 'Id Status External', onclick: function () { editor.insertContent("[OT][IDESTADOEXTERNO]"); } },
                        { text: 'LiteralName Status External', onclick: function () { editor.insertContent("[OT][NOMBREESTADOEXTERNO]"); } }
                    ]
                });
                editor.addMenuItem('Tipologia', {
                    text: 'Tipology',
                    context: 'woprop',
                    menu: [

                        { text: 'Id Project', onclick: function () { editor.insertContent("[OT][IDPROYECTO]"); } },
                        { text: 'LiteralName Project', onclick: function () { editor.insertContent("[OT][NOMBREPROYECTO]"); } },
                        { text: 'Id Type Ot', onclick: function () { editor.insertContent("[OT][IDTIPOOT]"); } }
                    ]
                });
                editor.addMenuItem('Orden', {
                    text: 'WorkOrder',
                    context: 'woprop',
                    menu: [

                        { text: 'CreationDate', onclick: function () { editor.insertContent("[OT][CREATIONDATE][yyyy-MM-dd hh:mm]"); } },
                        { text: 'ActuationDate', onclick: function () { editor.insertContent("[OT][ACTUATIONDATE][yyyy-MM-dd hh:mm]"); } },
                        { text: 'ReceptionDate', onclick: function () { editor.insertContent("[OT][HORARECOLLIDA][yyyy-MM-dd hh:mm]"); } },
                        { text: 'TimeCollected Client', onclick: function () { editor.insertContent("[OT][HORATANCAMENTCLIENT][yyyy-MM-dd hh:mm]"); } },
                        { text: 'TimeClosingSalto', onclick: function () { editor.insertContent("[OT][HORATANCAMENTSALTO][yyyy-MM-dd hh:mm]"); } },
                        { text: 'TimeAssign', onclick: function () { editor.insertContent("[OT][HORAASSIGNACIO][yyyy-MM-dd hh:mm]"); } },
                        { text: 'DataStopCronoSla', onclick: function () { editor.insertContent("[OT][DATAATURADACRONOSLA][yyyy-MM-dd hh:mm]"); } },
                        { text: 'DataAnswerSla', onclick: function () { editor.insertContent("[OT][DATARESPOSTASLA][yyyy-MM-dd hh:mm]"); } },
                        { text: 'DataResolutionSla', onclick: function () { editor.insertContent("[OT][DATARESOLUCIOSLA][yyyy-MM-dd hh:mm]"); } },
                        { text: 'DataPenalizationWithoutAnswerSla', onclick: function () { editor.insertContent("[OT][DATAPENALITZACIOSENSERESPOSTASLA][yyyy-MM-dd hh:mm]"); } },
                        { text: 'DataPenalizationWithoutResolutionSla', onclick: function () { editor.insertContent("[OT][DATAPENALITZACIOSENSERESOLUCIOSLA][yyyy-MM-dd hh:mm]"); } },
                        { text: 'ActuationEndDate', onclick: function () { editor.insertContent("[OT][ACTUATIONENDDATE][yyyy-MM-dd hh:mm]"); } },
                        { text: 'IsActuationFixed', onclick: function () { editor.insertContent("[OT][ISACTUATIONDATEFIXED][yyyy-MM-dd hh:mm]"); } },
                        { text: 'ClosingOtDates', onclick: function () { editor.insertContent("[OT][CLOSINGOTDATES][yyyy-MM-dd hh:mm]"); } },
                        { text: 'AccountingClosingDate', onclick: function () { editor.insertContent("[OT][ACCOUNTINGCLOSINGDATE][yyyy-MM-dd hh:mm]"); } },
                        { text: 'ClientClosingOTDates', onclick: function () { editor.insertContent("[OT][CLIENTCLOSINGOTDATES][yyyy-MM-dd hh:mm]"); } },
                        { text: 'InternalCreationDate', onclick: function () { editor.insertContent("[OT][INTERNALCREATIONDATE][yyyy-MM-dd hh:mm]"); } },
                        { text: 'SystemDateWhenOtClosed', onclick: function () { editor.insertContent("[OT][SYSTEMDATEWHENOTCLOSED][yyyy-MM-dd hh:mm]"); } }
                    ]
                });
                ////////////////////////////////////////////////////Formulario/////////////////////////////////////////////////////////////////////////////
                editor.addMenuItem('FormId', {
                    text: 'Id',
                    context: 'formprop',
                    onclick: function () { editor.insertContent("[FORM][ID]"); }
                });

                editor.addMenuItem('FormCreationDate', {
                    text: 'CreationDate',
                    context: 'formprop',
                    onclick: function () { editor.insertContent("[FORM][CREATIONDATE][yyyy-MM-dd hh:mm]"); }
                });
                editor.addMenuItem('CodigosCierre', {
                    text: 'ClosingCode',
                    context: 'formprop',
                    onclick: function () { editor.insertContent("[FORM][CODIGOSCIERRE]"); }
                });
                //                 editor.addMenuItem('TiempoOnSite', {
                //                 text: ' = I18N.Resource.OnSiteTime ',
                //                 context: 'formprop',
                //                 onclick: function () { editor.insertContent("[FORM][ONSITETIME]"); }
                //              });
                editor.addMenuItem('DniPersona', {
                    text: 'DNI Person',
                    context: 'formprop',
                    onclick: function () { editor.insertContent("[FORM][DNIPERSONA]"); }
                });
                editor.addMenuItem('NomPersona', {
                    text: 'FullName Person',
                    context: 'formprop',
                    onclick: function () { editor.insertContent("[FORM][NOMPERSONA]"); }
                });

                editor.addButton('currentdate', {
                    icon: 'insertdatetime',
                    //image: 'http://p.yusukekamiyamane.com/icons/search/fugue/icons/calendar-blue.png',
                    tooltip: "Insert Current Date",
                    onclick: function () {
                        var html = toTimeHtml(new Date());
                        editor.insertContent(html);
                    }
                });
            }
        });
    }
    
    function WorkForm(options) {
        
        var wopropText = "";
        options.templates.forEach(function (entri) { if (entri.Type == 1) { wopropText = wopropText + " " + (entri.Name); } })

        var formpropText = "";
        options.templates.forEach(function (entri) { if (entri.Type == 2) { formpropText = formpropText + " " + (entri.Name); } })

        tinymce.init(options, {
            selector: 'textarea',
            menu: {
                file: { title: 'File', items: 'newdocument' },
                edit: { title: 'Edit', items: 'undo redo | cut copy paste pastetext | selectall' },
                insert: { title: 'Insert', items: 'link media | template hr' },
                format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript | formats | removeformat' },
                table: { title: 'Table', items: 'inserttable tableprops deletetable | cell row column' },
                tools: { title: 'Tools', items: 'spellchecker code' },
                woprop: { title: 'WorkOrder', items: wopropText },
                formprop: { title: 'Form', items: formpropText }
            },
            menubar: true,
            plugins: 'code link image table',
            toolbar: [
                'undo redo | cut copy paste | styleselect | fontselect fontsizeselect | bold italic underline | bullist numlist | link image | table | alignleft aligncenter alignright alignjustify | code'
            ],

            setup: function (editor, options) {
                var i = 0;
                for (i; i < options.templates.length; ++i) {
                    if (options.templates[i].Type == 1) {
                        editor.addMenuItem(options.templates[i].Name, {
                            text: options.templates[i].Name,
                            context: 'woprop',
                            onclick: function () { editor.insertContent(options.templates[i].Template); }
                        });
                    }
                    if (options.templates[i].Type == 2) {
                        editor.addMenuItem(options.templates[i].Name, {
                            text: options.templates[i].Name,
                            context: 'formprop',
                            onclick: function () { editor.insertContent(options.templates[i].Template); }
                        });
                    }
                }
            }
        });
    }

    return {
        Init: init
    };
})();