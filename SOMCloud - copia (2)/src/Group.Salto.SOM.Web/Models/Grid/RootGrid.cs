using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Grid
{
    public class RootGrid
    {
        public RootGrid()
        {
            Rows = new List<Row>();
            Head = new List<Head>();
        }

        public int Pos { get; set; }
        public int Total_count { get; set; }
        public List<Row> Rows { get; set; }
        public List<Head> Head { get; set; }
    }
}