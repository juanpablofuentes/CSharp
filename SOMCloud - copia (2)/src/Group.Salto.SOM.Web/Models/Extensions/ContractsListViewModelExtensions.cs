using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Contracts;
using Group.Salto.SOM.Web.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ContractsListViewModelExtensions
    {
        public static ContractListViewModel ToViewModel(this ContractsListDto source)
        {
            ContractListViewModel result = null;
            if (source != null)
            {
                result = new ContractListViewModel()
                {
                    Id = source.Id,
                    Object = source.Object,
                    Active = source.Active,
                    Client = source.Client
                };
            }
            return result;
        }

        public static IList<ContractListViewModel> ToViewModel(this IList<ContractsListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}