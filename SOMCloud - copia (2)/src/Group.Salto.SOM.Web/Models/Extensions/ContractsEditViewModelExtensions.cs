using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using Group.Salto.ServiceLibrary.Common.Dtos.Contracts;
using Group.Salto.SOM.Web.Models.Contracts;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ContractsEditViewModelExtensions
    {
        public static ContractDto ToDto(this ContractEditViewModel source)
        {
            ContractDto result = null;
            if (source != null)
            {
                result = new ContractDto()
                {
                    Id = source.Id,
                    Object = source.Object,
                    Reference = source.Reference,
                    ContractTypeId = source.ContractTypeId,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    ContractUrl = source.PhysicalAddress,
                    Signer = source.Signer,
                    Active = source.Active,
                    ClientId = source.ClientId,
                    PeopleId = source.PeopleId,
                    Observations = source.Observations,
                    ContactsSelected = source.ContactsSelected.ToContactsContractsDto()
                };  
            }
            return result;
        }

        public static ResultViewModel<ContractEditViewModel> ToViewModel(this ResultDto<ContractDto> source)
        {
            var response = source != null ? new ResultViewModel<ContractEditViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ContractEditViewModel ToViewModel(this ContractDto source)
        {
            ContractEditViewModel result = null;
            if (source != null)
            {
                result = new ContractEditViewModel();
                result.Id = source.Id;
                result.Object = source.Object;
                result.Reference = source.Reference;
                result.ContractTypeId = source.ContractTypeId;
                result.StartDate = source.StartDate;
                result.EndDate = source.EndDate;
                result.PhysicalAddress = source.ContractUrl;
                result.Signer = source.Signer;
                result.Active = source.Active;
                result.ClientId = source.ClientId;
                result.PeopleId = source.PeopleId;
                result.Observations = source.Observations;
                result.ContactsSelected = source.ContactsSelected.ToContactsEditViewModel();
            }
            return result;
        }

        public static ContactsEditViewModel ToContactsEditViewModel(this ContactsDto source)
        {
            ContactsEditViewModel result = null;
            if (source != null)
            {
                result = new ContactsEditViewModel()
                {
                    ContactsId = source.Id,
                    Name = source.Name,
                    FirstSurname = source.FirstSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    Position = source.Position,
                    FullName = source.FullName
                };
            }
            return result;
        }

        public static IList<ContactsEditViewModel> ToContactsEditViewModel(this IList<ContactsDto> source)
        {
            return source?.MapList(pk => pk.ToContactsEditViewModel());
        }

        public static ContactsDto ToContactsContractsDto(this ContactsEditViewModel source)
        {
            ContactsDto result = null;
            if (source != null)
            {
                result = new ContactsDto()
                {
                    Id = source.ContactsId,
                    Name = source.Name,
                    FirstSurname = source.FirstSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    Position = source.Position,
                    FullName = source.FullName,
                };
            }
            return result;
        }

        public static IList<ContactsDto> ToContactsContractsDto(this IList<ContactsEditViewModel> source)
        {
            return source?.MapList(pk => pk.ToContactsContractsDto());
        }
    }
}