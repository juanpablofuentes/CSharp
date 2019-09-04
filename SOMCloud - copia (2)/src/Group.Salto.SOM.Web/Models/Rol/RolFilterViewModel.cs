namespace Group.Salto.SOM.Web.Models.Rol
{
    public class RolFilterViewModel : BaseFilter
    {
        public RolFilterViewModel()
        {
            OrderBy = nameof(Name);
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}