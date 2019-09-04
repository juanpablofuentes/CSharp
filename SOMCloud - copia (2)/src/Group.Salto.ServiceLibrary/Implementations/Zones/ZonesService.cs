using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Zones;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using Group.Salto.ServiceLibrary.Common.Dtos.Zones;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Zones
{
    public class ZonesService : BaseService, IZonesService
    {
        private readonly IZonesRepository _zonesRepository;
        private readonly IZoneProjectRepository _zonesProjectRepository;
        private readonly IZoneProjectPostalCodeRepository _zonesProjectPostalCodeRepository;

        public ZonesService(ILoggingService logginingService, IZonesRepository zonesRepository, IZoneProjectPostalCodeRepository zonesProjectPostalCodeRepository, IZoneProjectRepository zonesProjectRepository) : base(logginingService)
        {
            _zonesRepository = zonesRepository ?? throw new ArgumentNullException(nameof(IZonesRepository));
            _zonesProjectRepository = zonesProjectRepository ?? throw new ArgumentNullException(nameof(IZoneProjectRepository));
            _zonesProjectPostalCodeRepository = zonesProjectPostalCodeRepository ?? throw new ArgumentNullException(nameof(IZoneProjectPostalCodeRepository));
        }

        public ResultDto<IList<ZonesDto>> GetAllFiltered(ZonesFilterDto filter)
        {
            LogginingService.LogInfo($"Get All Zones Filtered");
            var query = _zonesRepository.GetAllWithZoneProject();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            var data = query.ToList().ToListDto().AsQueryable();
            data = OrderBy(data, filter);           
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        public ResultDto<ZonesDto> GetByIdWithZoneProjects(int id)
        {
            LogginingService.LogInfo($"Get All Zones Filtered");
            var query = _zonesRepository.GetByIdWithZoneProject(id);
            return ProcessResult(query.ToDto());
        }

        public ResultDto<ZonesDto> CreateZones(ZonesDto source,IList<ZoneProjectPostalCodeDto> SelectedPostalCodes)
        {
            LogginingService.LogInfo($"Create Zones");
            var newZones = source.ToEntity();                    
            if (newZones != null)
            {
                source = GetSelectedPostalCodes(source, SelectedPostalCodes);
                newZones = AssignZoneProject(newZones, source.ZoneProject);
            }
            var result = _zonesRepository.CreateZones(newZones);            
            var finalData = ProcessResult(result.Entity?.ToDto(), result);
            return finalData ?? new ResultDto<ZonesDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };           
        }

        public ResultDto<ZonesDto> UpdateZones(ZonesDto source,IList<ZoneProjectPostalCodeDto> SelectedPostalCodes)
        {
            LogginingService.LogInfo($"Update Zones");
            ResultDto<ZonesDto> result = null;
            var findZone = _zonesRepository.GetByIdWithZoneProject(source.Id);
           
            if (findZone != null)
            {
                source = GetSelectedPostalCodes(source, SelectedPostalCodes);
                var updatedZone = findZone.Update(source);                
                updatedZone = AssignZoneProject(updatedZone, source.ZoneProject);
                updatedZone = AssignToDeleteZoneProject(updatedZone, source);
                var resultRepository = _zonesRepository.UpdateZones(updatedZone);
                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }

            return result ?? new ResultDto<ZonesDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteZone(int id)
        {
            LogginingService.LogInfo($"DeleteZone by id {id}");
            ResultDto<bool> result = null;
            var zone = _zonesRepository.GetByIdWithZoneProject(id);
            if (zone != null && zone.PreconditionsLiteralValues.Count == 0)
            {
                zone = DeleteZoneProjectsFromZone(zone);
                var resultSave = _zonesRepository.DeleteZone(zone);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }

        public IList<BaseNameIdDto<int>> GetAllkeyValues()
        {
            LogginingService.LogInfo($"Get all Key Values Zones");
            var query = _zonesRepository.GetAllKeyValues();
            var data = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
            return data;
        }

        public ResultDto<List<MultiSelectItemDto>> GetZoneMultiSelect(List<int> selectItems)
        {
            LogginingService.LogInfo($"GetZoneMultiSelect");
            IEnumerable<BaseNameIdDto<int>> zones = GetAllkeyValues();
            return GetMultiSelect(zones, selectItems);
        }

        public string GetNamesComaSeparated(List<int> ids)
        {
            List<string> names = _zonesRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private ZonesDto GetSelectedPostalCodes(ZonesDto source,IList<ZoneProjectPostalCodeDto> postalcodes)
        {
            var postals = new List<ZoneProjectPostalCodeDto>();
            if (postalcodes != null && source.ZoneProject != null)
            {               
                    foreach (var zp in source.ZoneProject)
                    {
                        foreach (var posco in postalcodes)
                        {
                            if (posco.ZoneProjectId == zp.ZoneProjectId)
                            {
                                if (zp.PostalCodes == null)
                                {
                                    zp.PostalCodes = postals;
                                    zp.PostalCodes.Add(posco);
                                }
                                else
                                {
                                    zp.PostalCodes.Add(posco);
                                }
                            }
                        }
                    }                
            }
            return source;
        }

        private Entities.Tenant.Zones AssignZoneProject(Entities.Tenant.Zones entity, IList<ZoneProjectDto> models)
        {
            if (models != null && models.Any())
            {
                entity.ZoneProject = entity.ZoneProject ?? new List<Entities.Tenant.ZoneProject>();
                foreach (var mod in models)
                {
                    var temp = entity.ZoneProject.SingleOrDefault(x => x.Id == mod.ZoneProjectId);
                   
                    if (temp != null)
                    {                       
                        temp.ZoneId = mod.ZoneId;
                        temp.ProjectId = mod.ProjectId;
                        temp.UpdateDate = DateTime.UtcNow;
                        temp = AssignToAddPostalCode(temp, mod);
                        temp = AssignToUpdatePostalCode(temp, mod);
                        temp = AssignToDeletePostalCode(temp, mod);
                    }
                    else
                    {
                        if (mod.ProjectId == 0)
                        {
                            var newZP = new Entities.Tenant.ZoneProject()
                            {
                                ZoneId = mod.ZoneId,
                                ProjectId = null,
                                UpdateDate = DateTime.UtcNow,
                                
                            };
                            newZP = AssignToAddPostalCode(newZP, mod);
                            newZP = AssignToUpdatePostalCode(newZP, mod);
                            newZP = AssignToDeletePostalCode(newZP, mod);
                            entity.ZoneProject.Add(newZP);

                        }
                        else
                        {
                            var newZP = new Entities.Tenant.ZoneProject()
                            {
                                ZoneId = mod.ZoneId,
                                ProjectId = mod.ProjectId,
                                UpdateDate = DateTime.UtcNow,
                                
                            };
                            newZP = AssignToAddPostalCode(newZP, mod);
                            newZP = AssignToUpdatePostalCode(newZP, mod);
                            newZP = AssignToDeletePostalCode(newZP, mod);
                            entity.ZoneProject.Add(newZP);
                        }                        
                    }
                }
            }
            return entity;
        }

        private Entities.Tenant.Zones AssignToDeleteZoneProject(Entities.Tenant.Zones entity, ZonesDto zoneDto)
        {
            if (zoneDto.ZoneProject != null)
            {
                IList<ZoneProjectDto> forDelete = zoneDto.ZoneProject.Where(x => x.State == "D").ToList();
                if (forDelete != null && forDelete.Any())
                {
                    foreach (ZoneProjectDto row in forDelete)
                    {
                        Entities.Tenant.ZoneProject entityToDelete = entity.ZoneProject.Where(x => x.Id == row.ZoneProjectId).FirstOrDefault();
                        if (entityToDelete != null)
                        {
                            if(entityToDelete.ZoneProjectPostalCode!= null)
                            {
                                foreach(Entities.Tenant.ZoneProjectPostalCode pos in entityToDelete.ZoneProjectPostalCode.ToList())
                                {
                                    _zonesProjectPostalCodeRepository.DeletePostalCodeOnContext(pos);
                                    entity.ZoneProject.FirstOrDefault(x=>x.Id == row.ZoneProjectId).ZoneProjectPostalCode.Remove(pos);
                                }
                            }
                            _zonesProjectRepository.DeleteZoneProject(entityToDelete);
                            entity.ZoneProject.Remove(entityToDelete);
                        }
                    }
                }
            }
            return entity;
        }

        private Entities.Tenant.ZoneProject AssignToAddPostalCode(Entities.Tenant.ZoneProject entity, ZoneProjectDto zoneProjectDto)
        {
            if (zoneProjectDto.PostalCodes != null)
            {
                IList<ZoneProjectPostalCodeDto> forInsert = zoneProjectDto.PostalCodes.Where(x => x.State == "C" && x.ZoneProjectId == zoneProjectDto.ZoneProjectId).ToList();
                if (forInsert != null && forInsert.Any())
                {
                    entity.ZoneProjectPostalCode = entity.ZoneProjectPostalCode ?? new List<Entities.Tenant.ZoneProjectPostalCode>();
                    IList<Entities.Tenant.ZoneProjectPostalCode> localPostalCodes = forInsert.ToEntity();
                    foreach (Entities.Tenant.ZoneProjectPostalCode row in localPostalCodes)
                    {
                        if (!CheckPostalCodeExists(row.PostalCode))
                        {
                            entity.ZoneProjectPostalCode.Add(row);
                        }
                    }
                }
            }
            return entity;
        }

        private Entities.Tenant.ZoneProject AssignToUpdatePostalCode(Entities.Tenant.ZoneProject entity, ZoneProjectDto zoneProjectDto)
        {
            if (zoneProjectDto.PostalCodes != null)
            {
                IList<ZoneProjectPostalCodeDto> forUpdate = zoneProjectDto.PostalCodes.Where(x => x.State == "U" && x.ZoneProjectId == zoneProjectDto.ZoneProjectId).ToList();
                if (forUpdate != null && forUpdate.Any())
                {
                    foreach (Entities.Tenant.ZoneProjectPostalCode row in entity.ZoneProjectPostalCode)
                    {
                        ZoneProjectPostalCodeDto localps = forUpdate.Where(x => x.PostalCodeId == row.Id).FirstOrDefault();
                        if (localps != null)
                        {
                            localps.UpdateTime = DateTime.UtcNow;
                            row.UpdatePostalCode(localps.ToEntity());
                        }
                    }
                }
            }
            return entity;           
        }

        private bool CheckPostalCodeExists(string PostalCode)
        {
            var result = _zonesProjectPostalCodeRepository.CheckPostalCodeExists(PostalCode);
            if (result != null) return true;
            return false;
        }

        private Entities.Tenant.ZoneProject AssignToDeletePostalCode(Entities.Tenant.ZoneProject entity, ZoneProjectDto zoneProjectDto)
        {
            if (zoneProjectDto.PostalCodes != null)
            {
                IList<ZoneProjectPostalCodeDto> forDelete = zoneProjectDto.PostalCodes.Where(x => x.State == "D" && x.ZoneProjectId == zoneProjectDto.ZoneProjectId).ToList();
                if (forDelete != null && forDelete.Any())
                {

                    foreach (ZoneProjectPostalCodeDto row in forDelete)
                    {
                        Entities.Tenant.ZoneProjectPostalCode entityToDelete = entity.ZoneProjectPostalCode.Where(x => x.Id == row.PostalCodeId).FirstOrDefault();
                        if (entityToDelete != null)
                        {
                            _zonesProjectPostalCodeRepository.DeletePostalCodeOnContext(entityToDelete);
                            entity.ZoneProjectPostalCode.Remove(entityToDelete);
                        }
                    }
                }
            }
            return entity;
        }

        private Entities.Tenant.Zones DeleteZoneProjectsFromZone(Entities.Tenant.Zones localZones)
        {
            if (localZones.ZoneProject!= null)
            {
                foreach (var row in localZones.ZoneProject.ToList())
                {
                    if (row.ZoneProjectPostalCode != null)
                    {
                        foreach (var pos in row.ZoneProjectPostalCode.ToList())
                        {
                            _zonesProjectPostalCodeRepository.DeletePostalCodeOnContext(pos);
                        }
                    }                   
                    _zonesProjectRepository.DeleteZoneProject(row);
                }
            }
            return localZones;
        }

        private IQueryable<ZonesDto> OrderBy(IQueryable<ZonesDto> query, ZonesFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Zones");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}