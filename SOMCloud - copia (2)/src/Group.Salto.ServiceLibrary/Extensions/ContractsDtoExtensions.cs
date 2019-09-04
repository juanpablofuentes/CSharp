using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Contracts;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ContractsDtoExtensions
    {
        public static ContractDto ToDto(this Contracts source)
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
                    ContractUrl = source.ContractUrl,
                    Signer = source.Signer,
                    Active = source.Active,
                    ClientId = source.ClientId,
                    PeopleId = source.PeopleId,
                    Observations = source.Observations,
                    ContactsSelected = source.ContractContacts?.ToList()?.ToContractContactsDto(),
                };
            }

            return result;
        }

        public static ContractsListDto ToListDto(this Contracts source)
        {
            ContractsListDto result = null;
            if (source != null)
            {
                result = new ContractsListDto();
                source.ToListDto(result);
            }

            return result;
        }

        public static void ToListDto(this Contracts source, ContractsListDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Object = source.Object;
                target.Active = source.Active;
                target.Client = source.Client.CorporateName;
            }
        }

        public static IList<ContractsListDto> ToListDto(this IList<Contracts> source)
        {
            return source?.MapList(c => c.ToListDto());
        }

        public static IList<ContractDto> ToDto(this IList<Contracts> source)
        {
            return source?.MapList(c => c.ToDto());
        }

        public static Contracts ToEntity(this ContractDto source)
        {
            Contracts result = null;
            if (source != null)
            {
                result = new Contracts()
                {
                    Id = source.Id,
                    Object = source.Object,
                    Reference = source.Reference,
                    ContractTypeId = source.ContractTypeId,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    ContractUrl = source.ContractUrl,
                    Signer = source.Signer,
                    Active = source.Active,
                    ClientId = source.ClientId,
                    PeopleId = source.PeopleId,
                    Observations = source.Observations
                };
            }

            return result;
        }

        public static void UpdatePeople(this Contracts target, Contracts source)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Object = source.Object;
                target.Reference = source.Reference;
                target.ContractTypeId = source.ContractTypeId;
                target.StartDate = source.StartDate;
                target.EndDate = source.EndDate;
                target.ContractUrl = source.ContractUrl;
                target.Signer = source.Signer;
                target.Active = source.Active;
                target.ClientId = source.ClientId;
                target.PeopleId = source.PeopleId;
                target.Observations = source.Observations;
            }
        }

        public static bool IsValid(this ContractDto source)
        {
            bool result = false;
            result = source != null && !string.IsNullOrEmpty(source.Object) && source.ContractTypeId != 0;

            return result;
        }
    } 
}