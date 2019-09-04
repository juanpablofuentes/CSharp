using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Symptoms
{
    public class SymptomChildDto : SymptomBaseDto
    {
        public IList<SymptomChildDto> Symptoms { get; set; }
    }
}