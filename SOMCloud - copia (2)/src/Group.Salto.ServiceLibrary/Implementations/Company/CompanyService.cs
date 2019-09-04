using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Company;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Company;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.Company
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWorkCenterRepository _workCenterRepository;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ILoggingService logginingService,
                                IDepartmentRepository departmentRepository,
                                IWorkCenterRepository workCenterRepository,
                                ICompanyRepository companyRepository) : base(logginingService)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException($"nameof(IDepartmentRepository)");
            _workCenterRepository = workCenterRepository ?? throw new ArgumentNullException(nameof(IWorkCenterRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(ICompanyRepository));
        }

        public ResultDto<IList<CompanyDto>> GetAllFiltered(CompanyFilterDto filter)
        {
            LogginingService.LogInfo($"Get Companies filtered");
            var query = _companyRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToDto());
        }

        public ResultDto<CompanyDetailDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get Companies by id {id}");
            var result = _companyRepository.GetByIdWithoutDeletedRelations(id)?.ToDetailDto();
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<CompanyDetailDto> Update(CompanyDetailDto company)
        {
            LogginingService.LogInfo($"Update Companies with id = {company.Id}");
            ResultDto<CompanyDetailDto> result = null;
            var localCompany = _companyRepository.GetByIdWithoutDeletedRelations(company.Id);
            if (localCompany != null)
            {
                localCompany.Name = company.Name;
                localCompany.CostKm = company.CostKm;
                localCompany = UpdateCompanyDepartments(localCompany, company.Departments);
                var workCentersToDelete =
                    localCompany.WorkCenters
                        .Where(x => company.WorkCentersSelected == null || !company.WorkCentersSelected.Any(y => y.Id == x.Id)).Select(x => x.Id).ToList();
                localCompany = DeleteWorkCenters(localCompany, workCentersToDelete);
                var resultSave = _companyRepository.UpdateCompany(localCompany);
                result = ProcessResult(resultSave.Entity?.ToDetailDto(), resultSave);
            }
            return result ?? new ResultDto<CompanyDetailDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = company,
            };
        }

        private Companies DeleteWorkCenters(Companies company, IEnumerable<int> workCentersId)
        {
            if (workCentersId != null && workCentersId.Any())
            {
                foreach (var workCenterToDeleteId in workCentersId)
                {
                    var workCenter = company.WorkCenters.SingleOrDefault(x => x.Id == workCenterToDeleteId);
                    if (workCenter != null)
                    {
                        company.WorkCenters.Remove(workCenter);
                        workCenter.WorkCenterPeople = null;
                        workCenter.People = null;
                        _workCenterRepository.DeleteWorkCenterContext(workCenter);
                    }
                }
            }
            return company;
        }

        public ResultDto<CompanyDetailDto> Create(CompanyDetailDto company)
        {
            LogginingService.LogInfo($"Create Companies");
            var companyToCreate = company.ToEntity();
            var resultSave = _companyRepository.CreateCompany(companyToCreate);
            return ProcessResult(resultSave.Entity.ToDetailDto(), resultSave);
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete Company by id {id}");
            ResultDto<bool> result = null;
            var localCompany = _companyRepository.GetByIdWithoutDeletedRelations(id);
            if (localCompany != null)
            {
                DeleteDepartments(localCompany, localCompany.Departments);
                DeleteWorkCenters(localCompany, localCompany.WorkCenters.Select(x => x.Id).ToList());
                localCompany.People = null;
                var resultSave = _companyRepository.DeleteCompany(localCompany);
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

        private IQueryable<Companies> OrderBy(IQueryable<Companies> query, CompanyFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }

        private Companies UpdateCompanyDepartments(Companies localCompany, IList<DepartmentDto> companyDepartments)
        {
            var departmentsToDelete = localCompany.Departments?.Where(d => companyDepartments == null ||
                                                                            !companyDepartments.Any() ||
                                                                            !companyDepartments.Any(dd => dd.Id == d.Id));
            var departmentsToAdd = companyDepartments?.Where(x => x.Id == 0).ToList().Select(x => new Departments()
            {
                Name = x.Name,
            }).ToList();
            var departmentsToUpdate = localCompany.Departments?.Where(d => companyDepartments != null &&
                                                                           companyDepartments.Any(dd => dd.Id == d.Id));
            UpdateDepartments(departmentsToUpdate, companyDepartments);
            AddDepartments(localCompany, departmentsToAdd);
            DeleteDepartments(localCompany, departmentsToDelete.Where(d => !d.IsDeleted));
            return localCompany;
        }

        private void DeleteDepartments(Companies localCompany, IEnumerable<Departments> departmentsToDelete)
        {
            foreach (var department in departmentsToDelete.ToList())
            {
                department.People = null;
                _departmentRepository.DeleteContextDepartment(department);
                localCompany.Departments.Remove(department);
            }
        }

        private void AddDepartments(Companies localCompany, IList<Departments> departmentsToAdd)
        {
            if (localCompany != null && departmentsToAdd != null && departmentsToAdd.Any())
            {
                localCompany.Departments = localCompany.Departments ?? new List<Departments>();
                foreach (var department in departmentsToAdd)
                {
                    localCompany.Departments.Add(department);
                }
            }
        }

        private void UpdateDepartments(IEnumerable<Departments> target, IList<DepartmentDto> source)
        {
            if (target != null && source != null && source.Any())
            {
                foreach (var localDepartment in target)
                {
                    var department = source.Single(x => x.Id == localDepartment.Id);
                    localDepartment.UpdateDepartment(department);
                }
            }
        }

        public IList<BaseNameIdDto<int>> GetAllNotDeleteKeyValues()
        {
            LogginingService.LogInfo($"Get Companies Key Value");
            var data = _companyRepository.GetAllNotDeleteKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetDepartmentsByCompanyIdKeyValues(int? companyId)
        {
            LogginingService.LogInfo($"Get departments Key Value");
            var data = _departmentRepository.GetByCompanyKeyValues(companyId);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }
    }
}