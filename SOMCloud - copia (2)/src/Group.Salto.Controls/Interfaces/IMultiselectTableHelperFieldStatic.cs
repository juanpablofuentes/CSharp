namespace Group.Salto.Controls.Interfaces
{
    public interface IMultiselectTableHelperFieldStatic<TRoot> : IMultiselectTableHelperField<TRoot> where TRoot : class
    {
        string Text { get; set; }
        string HeaderText { get; set; }
    }
}
