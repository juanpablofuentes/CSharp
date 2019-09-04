using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClients;
using Group.Salto.SOM.Web.Models.Contracts;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.FinalClients;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class FinalClientsDetailViewModelExtension
    {
        public static FinalClientsDetailDto ToDetailDto(this FinalClientsDetailViewModel source)
        {
            var result = new FinalClientsDetailDto();
            if (source != null)
            {
                result.Id = source.Id;
                result.Name = source.Name;
                result.Nif = source.Nif;
                result.Phone1 = source.Phone1;
                result.Phone2 = source.Phone2;
                result.Phone3 = source.Phone3;
                result.Fax = source.Fax;
                result.Observations = source.Observations;
                result.Description = source.Description;
                result.Status = source.Status;
                result.PeopleCommercialId = source.SelectedComercialId ?? null;
                result.OriginId = (int)source.SelectedProcedenciaId;
                result.Code = source.Code;
                result.Contacts = source.Contacts.ToContactsContractsDto();
            }
            return result;
        }

        public static FinalClientsDetailViewModel ToDetailViewModel(this FinalClientsDetailDto source)
        {
            var result = new FinalClientsDetailViewModel();
            if (source != null)
            {
                result.Id = source.Id;
                result.Name = source.Name;
                result.Nif = source.Nif;
                result.Phone1 = source.Phone1;
                result.Phone2 = source.Phone2;
                result.Phone3 = source.Phone3;
                result.Fax = source.Fax;
                result.Observations = source.Observations;
                result.Description = source.Description;
                result.Status = source.Status;
                result.PeopleCommercialId = source.PeopleCommercialId ?? null;
                result.SelectedComercialId = source.PeopleCommercialId ?? null;
                result.OriginId = source.OriginId;
                result.SelectedProcedenciaId = source.OriginId;
                result.Code = source.Code;
                result.Contacts = source.Contacts.ToContactsEditViewModel();
            }
            return result;
        }

        public static ResultViewModel<FinalClientsDetailViewModel> ToDetailViewModel(this ResultDto<FinalClientsDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<FinalClientsDetailViewModel>()
            {
                Data = source.Data.ToDetailViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }
    }
}