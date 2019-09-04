using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Grid
{
    public class Row
    {
        public Row()
        {
            Data = new List<string>();
        }

        public int Id { get; set; }
        public List<string> Data { get; set; }
    }
}