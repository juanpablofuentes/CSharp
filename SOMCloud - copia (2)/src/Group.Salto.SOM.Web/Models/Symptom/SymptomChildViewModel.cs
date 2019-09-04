using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Symptom
{
    public class SymptomChildViewModel : SymptomViewModel
    {
        public IList<SymptomChildViewModel> Childs { get; set; }
    }
}