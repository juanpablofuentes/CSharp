namespace Group.Salto.SOM.Web.Models.Grid
{
    public class BaseGridViewModel
    {
        public bool DefaultOrder { get; set; }
        public string OrderBy { get; set; }
        public bool IsAscending { get; set; }
        public int PosStart { get; set; }
        public int Count { get; set; }
    }
}