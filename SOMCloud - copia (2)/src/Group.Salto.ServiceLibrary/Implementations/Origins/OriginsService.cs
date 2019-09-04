using Group.Salto.Common;
using Group.Salto.Common.Constants.Origins;
using Group.Salto.Common.Helpers;
using Group.Salto.DataAccess.Repositories;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Origins;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Origins;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Origins
{
    public class OriginsService : BaseService, IOriginsService
    {
        private readonly IOriginsRepository _originsRepository;
        private readonly IWorkOrdersRepository _workOrdersRepository;

        public OriginsService(ILoggingService logginingService, IOriginsRepository originsRepository, IWorkOrdersRepository workOrdersRepository) : base(logginingService)
        {
            _originsRepository = originsRepository ?? throw new ArgumentNullException($"{nameof(IOriginsRepository)} is null");
            _workOrdersRepository = workOrdersRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrdersRepository)} is null");
        }

        public ResultDto<OriginsDto> CreateOrigin(OriginsDto source)
        {
            LogginingService.LogInfo($"Creat Origin");

            var newOrigin = source.ToEntity();
            var result = _originsRepository.CreateOrigin(newOrigin);

            return ProcessResult(result.Entity?.ToListDto(), result);
        }

        public ResultDto<bool> DeleteOrigin(int id)
        {
            LogginingService.LogInfo($"Delete Origin -- #{id}");

            List<ErrorDto> errors = new List<ErrorDto>();
            bool deleteResult = false;
            var originToDelete = _originsRepository.GetByIdNotDeleted(id);

            if (originToDelete != null)
            {
                deleteResult = _originsRepository.DeleteOrigin(originToDelete);
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = OriginsConstants.OriginsNotExistMessage });
            }

            return ProcessResult(deleteResult, errors);
        }

        public ResultDto<OriginsDto> UpdateOrigin(OriginsDto source)
        {
            LogginingService.LogInfo($"Update Origins");

            ResultDto<OriginsDto> result = null;

            var findOrigin = _originsRepository.GetByIdNotDeleted(source.Id);
            if (findOrigin != null)
            {
                var updateOrigin = findOrigin.Update(source);
                var resultRepository = _originsRepository.UpdateOrigin(updateOrigin);
                result = ProcessResult(resultRepository.Entity?.ToBaseDto(), resultRepository);
            }

            return result ?? new ResultDto<OriginsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<IList<OriginsDto>> GetAllFiltered(OriginsFilterDto filter)
        {
            var query = _originsRepository.GetAllNotDeleted();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = query.WhereIfNotDefault(filter.Observations, au => au.Observations.Contains(filter.Description));

            var data = query.MapList(x => x.ToBaseDto());
            data = OrderBy(data.AsQueryable(), filter).ToList();
            return ProcessResult(data);
        }

        private IQueryable<OriginsDto> OrderBy(IQueryable<OriginsDto> query, OriginsFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Observations);
            return query;
        }

        public ResultDto<OriginsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Origins");
            var data = _originsRepository.GetByIdNotDeleted(id);
            return ProcessResult(data.ToListDto());
        }

        public ResultDto<bool> CanDelete(int id)
        {
            return ProcessResult(_workOrdersRepository.ExistOriginId(id));
        }

        public IList<BaseNameIdDto<int>> GetAllOriginKeyValues()
        {
            LogginingService.LogInfo($"Get Ogigins Key Value");
            var data = _originsRepository.GetAllOriginKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }
    }
}