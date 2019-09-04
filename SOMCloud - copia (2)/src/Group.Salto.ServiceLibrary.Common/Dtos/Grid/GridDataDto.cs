using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Grid
{
    public class GridDataDto
    {
        public GridDataDto()
        {
            Data = new List<string>();
        }

        public int Id { get; set; }
        public List<string> Data { get; set; }
    }
}