using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.Symptom;

namespace Group.Salto.SOM.Web.Models.Symptom
{
    public class SymptomsViewModel
    {
        public MultiItemViewModel<SymptomViewModel, int> Symptoms { get; set; }
        public SymptomFilterViewModel SymptomFilter { get; set; }
    }
}