namespace Group.Salto.SOM.Web.Models.MultiCombo
{
    public class MultiComboViewModel<TId, TIdSecondary>
    {
        public TId Value { get; set; }
        public TIdSecondary ValueSecondary { get; set; }
        public string Text { get; set; }
        public string TextSecondary { get; set; }
        public bool CanDelete { get; set; } = true;
    }
}