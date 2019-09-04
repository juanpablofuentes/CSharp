using Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges;
using Group.Salto.SOM.Web.Models.ServiceGauges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ServiceGaugesFilterViewModelIExtencion
    {
        public static ServiceGaugesFilterDto ToDto(this ServiceGaugesFilterViewModel source)
        {
            ServiceGaugesFilterDto result = null;
            if (source != null)
            {
                result = new ServiceGaugesFilterDto()
                {
                    WoId = source.WoId,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    Project = source.Project?.Text,
                    Client = source.Client?.Text,
                    ClientId= source.Clientint,
                    ProjectId = source.Projectint,
                    WoCategory = source.WoCategory,
                };
            }
            return result;
        }
    }
}