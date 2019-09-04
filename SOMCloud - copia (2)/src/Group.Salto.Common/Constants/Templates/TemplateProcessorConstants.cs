namespace Group.Salto.Common.Constants.Templates
{
    public static class TemplateProcessorConstants
    {
        public const string BillTemplateNullStringValue = "'[_BLANK]'";
        public const string TemplateWoId = "[OT][ID]";
        public const string TemplateWoClient = "[OT][OTCLIENT]";
        public const string TemplateWoClientSite = "[OT][OTCLIENTSITE]";
        public const string TemplateWoObservations = "[OT][OBSERVACIONS]";
        public const string TemplateWoRepair = "[OT][REPARACIO]";
        public const string TemplateWoSiteCode = "[OT][SITECODE]";
        public const string TemplateWoSiteName = "[OT][SITENAME]";
        public const string TemplateWoCategory = "[OT][CATEGORY]";
        public const string TemplateWoCategoryId = "[OT][CATEGORYID]";
        public const string TemplateFormId = "[FORM][ID]";
        public const string TemplateWoActionDate = "[OT][ACTUACIO]";
        public const string TemplateWoActuationEndDate = "[OT][FIACTUACIO]";

        //Form
        public const string TemplateFormClosingCode = "[FORM][CODIGOSCIERRE]";
        public const string TemplateFormDni = "[FORM][DNIPERSONA]";
        public const string TemplateFormFullName = "[FORM][NOMPERSONA]";
        public const string TemplateFormCreationDate = "[FORM][CREATIONDATE]";

        //Status
        public const string TemplateWoStatusId = "[OT][IDESTADO]";
        public const string TemplateWoStatusName = "[OT][NOMBREESTADO]";
        public const string TemplateWoExternalStatusId = "[OT][IDESTADOEXTERNO]";
        public const string TemplateWoExternalStatusName = "[OT][NOMBREESTADOEXTERNO]";

        //Typology
        public const string TemplateWoProjectId = "[OT][IDPROYECTO]";
        public const string TemplateWoProjectName = "[OT][NOMBREPROYECTO]";
        public const string TemplateWoTypeId = "[OT][IDTIPOOT]";

        //Location
        public const string TemplateWoLocationName = "[OT][LOCALIDAD]";
        public const string TemplateWoLocationProvince = "[OT][PROVINCIA]";
        public const string TemplateWoLocationPostalCode = "[OT][CODIGOPOSTAL]";
        public const string TemplateWoLocationDirection = "[OT][DIRECCION]";
        public const string TemplateWoLocationZone = "[OT][ZONA]";
        public const string TemplateWoLocationSubzone = "[OT][SUBZONA]";
        public const string TemplateWoArea = "[OT][AREA]";
        public const string TemplateWoLocationLatitude = "[OT][LATITUD]";
        public const string TemplateWoLocationLongitude = "[OT][LONGITUD]";

        //Usr
        public const string TemplateWoPersonDni = "[OT][DNIPERSONA]";
        public const string TemplateWoPersonFullname = "[OT][NOMPERSONA]";

        //WorkOrder
        public const string TemplateWoIsActuationFixed = "[OT][ISACTUATIONDATEFIXED]";

        public const string TemplateWoDateCreation = "[OT][CREATIONDATE]";
        public const string TemplateWoDateActuation = "[OT][ACTUATIONDATE]";
        public const string TemplateWoDatePickUp = "[OT][HORARECOLLIDA]";
        public const string TemplateWoDateClientClosed = "[OT][HORATANCAMENTCLIENT]";
        public const string TemplateWoDateClosing = "[OT][HORATANCAMENTSALTO]";
        public const string TemplateWoDateAssigned = "[OT][HORAASSIGNACIO]";
        public const string TemplateWoDateStopTimeSla = "[OT][DATAATURADACRONOSLA]";
        public const string TemplateWoDateSlaResponse = "[OT][DATARESPOSTASLA]";
        public const string TemplateWoDateResolutionSla = "[OT][DATARESOLUCIOSLA]";
        public const string TemplateWoDatePenalizationWithoutResponseSla = "[OT][DATAPENALITZACIOSENSERESPOSTASLA]";
        public const string TemplateWoDatePenalizationWithoutRelosutionSla = "[OT][DATAPENALITZACIOSENSERESOLUCIOSLA]";
        public const string TemplateWoDateActuationEnd = "[OT][ACTUATIONENDDATE]";
        public const string TemplateWoDateClosingOt = "[OT][CLOSINGOTDATES]";
        public const string TemplateWoDateClosingAccounting = "[OT][ACCOUNTINGCLOSINGDATE]";
        public const string TemplateWoDateClosingClient = "[OT][CLIENTCLOSINGOTDATES]";
        public const string TemplateWoDateInternalCreation = "[OT][INTERNALCREATIONDATE]";
        public const string TemplateWoDateSystemWhenOtClosed = "[OT][SYSTEMDATEWHENOTCLOSED]";

        //Date format
        public const string TemplateStringDateFormat = "dd/MM/yyyy HH:mm:ss";
        public const string TemplateStringTimeFormat = @"hh\:mm";

        //Extra field
        public const string TemplateExtraFieldIdentifier = "[FORM]";
        public const string TemplateExtraFieldIsBlank = "[FORM]";
        public const string TemplateExtraFieldTrueValue = "true";
        public const string TemplateExtraFieldFalseValue = "false";
        public const string TemplateExtraFieldHours = "hours";
        public const string TemplateExtraFieldHoursDecimal = "hoursdecimal";
        public const string TemplateExtraFieldMinute = "minutes";
        public const string TemplateExtraFieldDefaultValue = "-";
        public const string TemplateExtraFieldSelectorSeparator = ";";
        public const string TemplateExtraFieldSelectorLineBreak = "\n";
    }
}
