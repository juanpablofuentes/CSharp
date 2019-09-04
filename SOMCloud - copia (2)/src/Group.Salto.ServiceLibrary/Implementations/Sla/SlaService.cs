using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Sla;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Sla;
using Group.Salto.ServiceLibrary.Common.Dtos.StatesSla;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.sla
{
    public class SlaService : BaseService, ISlaService
    {
        private readonly ISlaRepository _slaRepository;
        private readonly IStatesSlaRepository _statesSlaRepository;

        public SlaService(ILoggingService logginingService, ISlaRepository slaRepository, IStatesSlaRepository statesSlaRepository) : base(logginingService)
        {
            _slaRepository = slaRepository ?? throw new ArgumentNullException(nameof(ISlaRepository));
            _statesSlaRepository = statesSlaRepository ?? throw new ArgumentNullException(nameof(IStatesSlaRepository));
        }

        public ResultDto<IList<SlaDto>> GetAllFiltered(SlaFilterDto filter)
        {
            LogginingService.LogInfo($"Get All sla Filtered");
            var query = _slaRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get SLA Key Value");
            var data = _slaRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<SlaDetailsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id sla");
            var data = _slaRepository.GetByIdWithStates(id);
            return ProcessResult(data.ToDetailDto());
        }

        public ResultDto<SlaDetailsDto> CreateSla(SlaDetailsDto source)
        {
            LogginingService.LogInfo($"Create Sla");
            var newSla = source.ToEntity();
            var result = _slaRepository.CreateSla(newSla);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public ResultDto<SlaDetailsDto> UpdateSla(SlaDetailsDto source)
        {
            LogginingService.LogInfo($"Update Sla");
            ResultDto<SlaDetailsDto> result = null;
            var findSla = _slaRepository.GetByIdWithStates(source.Id);

            if (findSla != null)
            {
                var updatedSla = findSla.Update(source);
                updatedSla = AssignStatesSla(updatedSla, source.StatesSla);
                var resultRepository = _slaRepository.UpdateSla(updatedSla);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }

            return result ?? new ResultDto<SlaDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteSla(int id)
        {
            LogginingService.LogInfo($"Delete sla by id {id}");
            ResultDto<bool> result = null;
            var sla = _slaRepository.GetByIdWithStates(id);
            if (sla == null)
            {
                return result ?? new ResultDto<bool>()
                {
                    Errors = new ErrorsDto()
                    {
                        Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                    },
                    Data = false,
                };
            }
            if (sla.WorkOrderCategories.Count == 0 && sla.WorkOrderTypes.Count == 0)
            {
                sla = RemoveAllStatesSla(sla);
                var resultSave = _slaRepository.DeleteSla(sla);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.ValidationError } }
                },
                Data = false,
            };
        }

        private Sla AssignStatesSla(Sla entity, IList<StatesSlaDto> statessla)
        {
            entity = RemoveAllStatesSla(entity);
            entity = AssignNewStatesSla(entity, statessla);
            return entity;
        }

        private Sla AssignNewStatesSla(Sla entity, IList<StatesSlaDto> statessla)
        {
            if (statessla != null && statessla.Any())
            {
                entity.StatesSla = entity.StatesSla ?? new List<StatesSla>();
                foreach (var states in statessla)
                {
                    entity.StatesSla.Add(new StatesSla
                    {
                        MinutesToTheEnd = states.MinutesToTheEnd,
                        RowColor = states.RowColor
                    });
                }
            }
            return entity;
        }

        private Sla RemoveAllStatesSla(Sla entity)
        {
            SlaDetailsDto dto = entity.ToDetailDto();
            foreach (StatesSlaDto item in dto.StatesSla)
            {
                StatesSla ent = entity.StatesSla.Where(x => x.SlaId == item.SlaId).FirstOrDefault();
                if (ent != null)
                {
                    _statesSlaRepository.DeleteOnContextStatesSla(ent);
                    entity.StatesSla.Remove(ent);
                }
            }
            return entity;
        }

        private IQueryable<Sla> OrderBy(IQueryable<Sla> query, SlaFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Sla");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}