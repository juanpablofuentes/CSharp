namespace Group.Salto.SOM.Web.Models
{
    public class FilterHeaderViewModel
    {
        public FilterHeaderViewModel()
        {
            ShowAddNewButon = false;
            AddNewButtonLink = "";
            parentId = 0;
        }

        public bool ShowAddNewButon { get; set; }
        public string AddNewButtonLink { get; set; }
        public int parentId { get; set; } 
    }
}
