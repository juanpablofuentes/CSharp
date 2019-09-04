using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.People;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PeopleCollection
{
    public class PeopleCollectionDto : PeopleCollectionBaseDto
    {
        public IList<PeopleSelectableDto> People { get; set; }
        public IList<PeopleSelectableDto> PeopleAdmin { get; set; }
    }
}