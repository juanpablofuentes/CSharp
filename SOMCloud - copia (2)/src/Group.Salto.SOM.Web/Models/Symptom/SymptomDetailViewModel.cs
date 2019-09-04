using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Symptom
{
    public class SymptomDetailViewModel
    {
        public SymptomViewModel Symptom { get; set; }
        public IList<SymptomChildViewModel> ChildSymptoms { get; set; }
    }
}