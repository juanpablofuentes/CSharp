using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection
{
    public class SymptomCollectionDto : SymptomCollectionBaseDto
    {
        public IList<int> SymptomSelected { get; set; }
    }
}