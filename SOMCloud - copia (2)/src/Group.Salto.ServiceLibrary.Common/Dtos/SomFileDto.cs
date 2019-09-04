using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public class SomFileDto
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Container { get; set; }
        public string Directory { get; set; }
        public string Name { get; set; }
        public string ContentMd5 { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
