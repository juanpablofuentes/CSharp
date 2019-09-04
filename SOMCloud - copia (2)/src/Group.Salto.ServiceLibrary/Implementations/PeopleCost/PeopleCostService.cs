using Group.Salto.Common;
using Group.Salto.Common.Constants.People;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCost;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCost;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.PeopleCost
{
    public class PeopleCostService : BaseService, IPeopleCostService
    {
        private readonly IPeopleCostRepository _peopleCostRepository;

        public PeopleCostService(ILoggingService logginingService,
                                IPeopleCostRepository peopleCostRepository) : base(logginingService)
        {
            _peopleCostRepository = peopleCostRepository ?? throw new ArgumentNullException($"{nameof(IPeopleCostRepository)} is null ");
        }

        public ResultDto<IList<PeopleCostDetailDto>> GetByPeopleId(int peopleId)
        {
            LogginingService.LogInfo($"Get People cost by PeopleId {peopleId}");
            IList<PeopleCostDetailDto> result = _peopleCostRepository.GetByPeopleIdNotDeleted(peopleId)?.ToPeopleCostDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<PeopleCostDetailDto> Create(PeopleCostDetailDto peopleCost)
        {
            LogginingService.LogInfo($"Creating new PeopleCost");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidatePeopleCost(peopleCost, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                Entities.Tenant.PeopleCost newContract = peopleCost.ToEntity();
                var result = _peopleCostRepository.CreatePeopleCost(newContract);
                return ProcessResult(result.Entity?.ToPeopleCostDto(), result);
            }

            return ProcessResult(peopleCost, errors);
        }

        public ResultDto<PeopleCostDetailDto> Update(PeopleCostDetailDto peoplecost)
        {
            LogginingService.LogInfo($"Update PeopleCost");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidatePeopleCost(peoplecost, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                Entities.Tenant.PeopleCost localPeopleCost = _peopleCostRepository.GetByIdNotDeleted(peoplecost.Id);

                if (localPeopleCost != null)
                {
                    ResultDto<PeopleCostDetailDto> result = null;

                    if (localPeopleCost != null)
                    {
                        localPeopleCost.UpdatePeopleCost(peoplecost.ToEntity());
                        var resultRepository = _peopleCostRepository.UpdatePeopleCost(localPeopleCost);
                        result = ProcessResult(resultRepository.Entity?.ToPeopleCostDto(), resultRepository);
                    }
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = PeopleConstants.PeopleCostNotExist });
                }
            }

            return ProcessResult(peoplecost, errors);
        }

        public ResultDto<bool> Delete(int Id)
        {
            LogginingService.LogInfo($"Delete PeopleCost {Id}");
            List<ErrorDto> errors = new List<ErrorDto>();
            bool deleteResult = false;

            var peopleCostToDelete = _peopleCostRepository.GetByIdNotDeleted(Id);
            if (peopleCostToDelete != null)
            {
                if (peopleCostToDelete != null)
                {
                    deleteResult = _peopleCostRepository.DeletePeopleCost(peopleCostToDelete);
                }
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = PeopleConstants.PeopleCostNotExist });
            }

            return ProcessResult(deleteResult, errors);
        }

        private bool ValidatePeopleCost(PeopleCostDetailDto peopleCost, out List<ErrorDto> result)
        {
            result = new List<ErrorDto>();
            IList<Entities.Tenant.PeopleCost> listPeopleCosts = _peopleCostRepository.GetByPeopleIdNotDeleted(peopleCost.PeopleId);
            if (!peopleCost.IsValid())
            {
                result.Add(new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                });
            }

            if (ExistCostsInSameDates(peopleCost, listPeopleCosts))
            {
                result.Add(new ErrorDto()
                {
                    ErrorMessageKey = nameof(PeopleCostDetailDto.StartDate),
                    ErrorType = ErrorType.DateRangeValidationError,
                });
            }

            return !result.Any();
        }

        public bool ExistCostsInSameDates(PeopleCostDetailDto peopleCost, IList<Entities.Tenant.PeopleCost> peopleCosts)
        {
            Entities.Tenant.PeopleCost costsInDdates = null;
            var evalPeopleCost = peopleCosts.Select(x => new Entities.Tenant.PeopleCost()
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate ?? DateTime.MaxValue,
                HourCost = x.HourCost,
            });
            if (!peopleCost.EndDate.HasValue)
            {
                costsInDdates = evalPeopleCost.Where(pc => (peopleCost.StartDate >= pc.StartDate && peopleCost.StartDate <= pc.EndDate) 
                                                        || (pc.StartDate >= peopleCost.StartDate)).FirstOrDefault();
            }
            else
            {
                costsInDdates = evalPeopleCost.Where(pc => (peopleCost.StartDate >= pc.StartDate && peopleCost.StartDate <= pc.EndDate)
                || (peopleCost.EndDate >= pc.StartDate && peopleCost.EndDate <= pc.EndDate)
                || (pc.StartDate >= peopleCost.StartDate && pc.StartDate <= peopleCost.EndDate)
                || (pc.EndDate >= peopleCost.StartDate && pc.EndDate <= peopleCost.EndDate)
                || (pc.EndDate == null && pc.StartDate <= peopleCost.StartDate)).FirstOrDefault();
            }

            if (costsInDdates != null)
            {
                if (peopleCost.HourCost.HasValue && costsInDdates.HourCost == peopleCost.HourCost) return false;
                return true;
            }

            return false;
        }
    }
}