using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.SubFamilies;
using Group.Salto.SOM.Web.Models.Families;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SubFamiliesViewModelExtensions
    {
        public static SubFamiliesViewModel ToSubFamiliesViewModelFamiliesSubFamilies(this SubFamiliesDto source)
        {
            SubFamiliesViewModel result = null;
            if (source != null)
            {
                result = new SubFamiliesViewModel()
                {
                    SubFamiliesId = source.Id,
                    SubFamiliesName = source.Nom,           
                    SubFamiliesDescription = source.Descripcio
                };
            }
            return result;
        }

        public static IList<SubFamiliesViewModel> ToSubFamiliesViewModelFamiliesSubFamilies(this IList<SubFamiliesDto> source)
        {
            return source?.MapList(sCK => sCK.ToSubFamiliesViewModelFamiliesSubFamilies());
        }

        public static SubFamiliesDto ToFamiliesSubFamiliesDto(this SubFamiliesViewModel source)
        {
            SubFamiliesDto result = null;
            if (source != null)
            {
                result = new SubFamiliesDto()
                {
                    Nom = source.SubFamiliesName,
                    Descripcio = source.SubFamiliesDescription,
                };
            }
            return result;
        }

        public static IList<SubFamiliesDto> ToFamiliesSubFamiliesDto(this IList<SubFamiliesViewModel> source)
        {
            return source?.MapList(sCK => sCK.ToFamiliesSubFamiliesDto());
        }

        public static SubFamiliesDto ToFamiliesEditSubFamiliesDto(this SubFamiliesViewModel source)
        {
            SubFamiliesDto result = null;
            if (source != null)
            {
                result = new SubFamiliesDto()
                {
                    Id = source.SubFamiliesId,
                    Nom = source.SubFamiliesName,
                    Descripcio = source.SubFamiliesDescription,
                };
            }
            return result;
        }

        public static IList<SubFamiliesDto> ToFamiliesEditSubFamiliesDto(this IList<SubFamiliesViewModel> source)
        {
            return source?.MapList(sCK => sCK.ToFamiliesEditSubFamiliesDto());
        }
    }
}