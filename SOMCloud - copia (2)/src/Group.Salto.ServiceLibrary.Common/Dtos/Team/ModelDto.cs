﻿namespace Group.Salto.ServiceLibrary.Common.Dtos.Team
{
    public class ModelDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public BrandDto Brand { get; set; }
    }
}