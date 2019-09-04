namespace Group.Salto.Controls.Interfaces
{
    public interface ITranslationTable
    {
        string GetTranslationText(string key, string culture);
        string GetTranslationText(string key);
    }
}