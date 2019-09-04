using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Families;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Families;
using Group.Salto.ServiceLibrary.Common.Dtos.SubFamilies;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Families
{
    public class FamiliesService : BaseFilterService, IFamiliesService
    {
        private readonly IFamiliesRepository _familiesRepository;
        private readonly ISubFamiliesRepository _subfamiliesRepository;
        private readonly IAssetsRepository _assetsRepository;

        public FamiliesService(
            ILoggingService logginingService,
            IFamiliesRepository familiesRepository,
            ISubFamiliesRepository subfamiliesRepository,
            IAssetsRepository assetsRepository,
            IFamiliesQueryFactory queryFactory) : base(queryFactory, logginingService)
        {
            _familiesRepository = familiesRepository ?? throw new ArgumentNullException($"{nameof(IFamiliesRepository)} is null ");
            _subfamiliesRepository = subfamiliesRepository ?? throw new ArgumentNullException($"{nameof(ISubFamiliesRepository)} is null ");
            _assetsRepository = assetsRepository ?? throw new ArgumentNullException($"{nameof(IAssetsRepository)} is null ");
        }

        public ResultDto<FamiliesDetailsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Families");
            var data = _familiesRepository.GetFamilieWithSubFamilies(id);
            return ProcessResult(data.ToDetailDto());
        }

        public ResultDto<IList<FamiliesDto>> GetAllFiltered(FamiliesFilterDto filter)
        {
            LogginingService.LogInfo($"Get All Families Filtered");
            var query = _familiesRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        public ResultDto<FamiliesDetailsDto> CreateFamilies(FamiliesDetailsDto source)
        {
            LogginingService.LogInfo($"Create Families");
            var newFamilies = source.ToEntity();
            var result = _familiesRepository.CreateFamilies(newFamilies);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public ResultDto<FamiliesDetailsDto> UpdateFamilies(FamiliesDetailsDto source)
        {
            LogginingService.LogInfo($"Update Families");
            ResultDto<FamiliesDetailsDto> result = null;
            var findFamilies = _familiesRepository.GetFamilieWithSubFamilies(source.Id);
            if (findFamilies == null)
            {
                return result ?? new ResultDto<FamiliesDetailsDto>()
                {
                    Errors = new ErrorsDto()
                    {
                        Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                    },
                    Data = source,
                };
            }
            if (CheckCanEraseSubFamilie(source,findFamilies))
            {
                var updatedFamilies = findFamilies.Update(source);
                updatedFamilies = AssignSubFamilies(updatedFamilies, source.SubFamiliesList);
                var resultRepository = _familiesRepository.UpdateFamilies(updatedFamilies);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }
            return result ?? new ResultDto<FamiliesDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.ValidationError } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteFamilies(int id)
        {
            LogginingService.LogInfo($"Delete families by id {id}");
            ResultDto<bool> result = null;
            var families = _familiesRepository.GetFamilieWithSubFamilies(id);
            if (families == null)
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
            if (families.SubFamilies.Select(x=>x.Assets) == null)
            {
                families = DeleteSubFamiliesFromFamilie(families);
                var resultSave = _familiesRepository.DeleteFamilies(families);
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

        private IQueryable<Entities.Tenant.Families> OrderBy(IQueryable<Entities.Tenant.Families> query, FamiliesFilterDto filter)
        {
            LogginingService.LogInfo($"Order By families");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }

        private Entities.Tenant.Families AssignSubFamilies(Entities.Tenant.Families entity, IList<SubFamiliesDto> subfamilies)
        {
            if (entity.SubFamilies != null)
            {
                foreach (var mod in entity.SubFamilies.ToList())
                {
                    _subfamiliesRepository.DeleteOnContextSubFamilie(mod);
                }
            }
            if (subfamilies != null && subfamilies.Any())
            {
                entity.SubFamilies = entity.SubFamilies ?? new List<Entities.Tenant.SubFamilies>();
                
                foreach (var sub in subfamilies)
                {
                        entity.SubFamilies.Add(new Entities.Tenant.SubFamilies()
                        {
                            Nom = sub.Nom,
                            Descripcio = sub.Descripcio,
                        });
                }
            }
            return entity;
        }

        private bool CheckCanEraseSubFamilie(FamiliesDetailsDto newFamilie, Entities.Tenant.Families oldFamilie)
        {
            if (oldFamilie.SubFamilies!=null && newFamilie.SubFamiliesList!=null) {
                var uids = newFamilie.SubFamiliesList.Select(x => x.Id).ToList();
                foreach (var fam in oldFamilie.SubFamilies.ToList())
                {
                    if (!uids.Contains(fam.Id))
                    {
                        var team = _assetsRepository.GetAssetBySubFamilieId(fam.Id);
                        if ( team.Count() != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private Entities.Tenant.Families DeleteSubFamiliesFromFamilie(Entities.Tenant.Families localFamilies)
        {
            if (localFamilies.SubFamilies != null)
            {
                foreach (var mod in localFamilies.SubFamilies.ToList())
                {
                    _subfamiliesRepository.DeleteOnContextSubFamilie(mod);
                }
            }
            return localFamilies;
        }
    }
}