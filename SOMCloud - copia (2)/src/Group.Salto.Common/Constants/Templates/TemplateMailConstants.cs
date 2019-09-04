using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Common.Constants.Templates
{
    public static class TemplateMailConstants
    {
        public const string WorkOrderId = "Id OT";
        public const string InternalId = "OT Cliente";
        public const string ExternalId = "OT Cliente Site";
        public const string ParentWoId = "OT Padre";
        public const string Project = "Proyecto";
        public const string WoType = "Tipo de OT";
        public const string WorkOrderCategory = "Categoria de OT";
        public const string CreationDate = "Fecha de Creación";
        public const string SlaEndDate = "Fecha de Fin SLA";
        public const string ClientClosingDate = "Fecha de Cierre Cliente";
        public const string Queue = "Cola";
        public const string ActuationDate = "Fecha de Actuación";
        public const string WoStatus = "Estado OT";
        public const string ExternalWoStatus = "Estado Externo de OT";
        public const string SaltoClient = "Cliente Saltó";
        public const string SiteClient = "Cliente Site";
        public const string Site = "Ubicación";
        public const string Address = "Dirección";
        public const string Phone1 = "Teléfono 1";
        public const string Phone2 = "Teléfono 2";
        public const string Phone3 = "Teléfono 3";
        public const string Asset = "Activo";
        public const string AssetUbication = "Ubicación del Activo";
        public const string AssetAddress = "Dirección del Activo";
        public const string Operator = "Operador";
        public const string Technician = "Técnico";
        public const string Reparation = "Reparación";
        public const string Observations = "Observaciones";

        public const string MailSubjectRegEx = "<.*?>";

        public const string TemplateWoStart = "[OT:";
        public const string TemplateWoClient = ", OT Cliente:";
        public const string TemplateWoClientSite = ", OT Cliente-Site:";
        public const string TemplateSpace = " ";
        public const string TemplateEnd = "] ";
        public const string TemplateClose = "> ";
        public const string TemplateCloseEnd = ">";

    }
}
