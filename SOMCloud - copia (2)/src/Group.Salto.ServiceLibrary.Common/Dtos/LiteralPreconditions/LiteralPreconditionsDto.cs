using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionLiteralValues;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions
{
    public class LiteralPreconditionsDto
    {
        public int Id { get; set; }
        public int PreconditionId { get; set; }
        public string NomCampModel { get; set; }
        public string ComparisonOperator { get; set; }
        public int? ExtraFieldId { get; set; }
        public string ExtraField { get; set; }
        public string Precondition { get; set; }
        public Guid PreconditionsTypeId { get; set; }
        public string PreconditionsTypeName { get; set; }
        public IList<PreconditionLiteralValuesDto> PreconditionsLiteralValues { get; set; }
        //TODO
        //public IList<BaseNameIdDto<int>> PreconditionLiteralValuesIdNameList { get; set; }
    }
}