using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Group.Salto.Common;
using Group.Salto.Common.Constants.SubContracts;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Subcontracts;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.Subcontracts
{
    public class SubContractService : BaseService, ISubContractService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly ISubContractRepository _subContractRepository;
        private readonly IKnowledgeRepository _knowledgeRepository;
        private readonly IPurchaseRateRepository _purchaseRateRepository;

        public SubContractService(ILoggingService logginingService,
                                  ISubContractRepository subContractRepository,
                                  IKnowledgeRepository knowledgeRepository,
                                  IPurchaseRateRepository purchaseRateRepository,
                                  IPeopleRepository peopleRepository) : base(logginingService)
        {
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException(nameof(IPeopleRepository));
            _subContractRepository = subContractRepository ?? throw new ArgumentNullException(nameof(ISubContractRepository));
            _knowledgeRepository = knowledgeRepository ?? throw new ArgumentNullException(nameof(IKnowledgeRepository));
            _purchaseRateRepository = purchaseRateRepository ?? throw new ArgumentNullException(nameof(IPurchaseRateRepository));
        }

        public ResultDto<IList<SubContractBaseDto>> GetAllFiltered(SubContractFilterDto filter)
        {
            var query = _subContractRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToBaseDto());
        }

        public ResultDto<SubContractDto> GetById(int id)
        {
            var localSubContract = _subContractRepository.GetById(id);
            return ProcessResult(localSubContract.ToDto(), localSubContract != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<SubContractDto> Create(SubContractDto model)
        {
            ResultDto<SubContractDto> result = null;
            var canAssignPeople = CanAssignPeople(model.PeopleSelected, out var peopleToAssign);
            if (canAssignPeople)
            {
                var entity = model.ToEntity();
                entity = AssignKnowledges(entity, model.KnowledgeSelected);
                entity = AssignPeople(entity, peopleToAssign, model.PeopleSelected?.Where(x => x.IsResponsable).ToList());
                entity = AssignPurchaseRate(entity, model.PurchaseRateId);
                var resultRepository = _subContractRepository.CreateSubContract(entity);
                result = ProcessResult(resultRepository?.Entity?.ToDto(), resultRepository);
            }
            else
            {
                result = ProcessResult(model, new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                    ErrorMessageKey = SubContractsConstants.SubContractCannotAssignPeople,
                });
            }

            return result;
        }

        public ResultDto<SubContractDto> Update(SubContractDto model)
        {
            ResultDto<SubContractDto> result = null;
            var canAssignPeople = CanAssignPeople(model.PeopleSelected, out var peopleToAssign, model.Id);
            if (canAssignPeople)
            {
                var localSubContract = _subContractRepository.GetById(model.Id);
                if (localSubContract != null)
                {
                    localSubContract.Update(model);
                    localSubContract = AssignKnowledges(localSubContract, model.KnowledgeSelected);
                    localSubContract = AssignPeople(localSubContract, peopleToAssign, model.PeopleSelected?.Where(x => x.IsResponsable).ToList());
                    localSubContract = AssignPurchaseRate(localSubContract, model.PurchaseRateId);
                    var resultRepository = _subContractRepository.UpdateSubContract(localSubContract);
                    result = ProcessResult(resultRepository.Entity.ToDto(), resultRepository);
                }
                else
                {
                    result = ProcessResult(model, new ErrorDto()
                    {
                        ErrorType = ErrorType.EntityNotExists,
                        ErrorMessageKey = SubContractsConstants.SubContractNotExists,
                    });
                }
            }
            else
            {
                result = ProcessResult(model, new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                    ErrorMessageKey = SubContractsConstants.SubContractCannotAssignPeople,
                });
            }

            return result;
        }

        private SubContracts AssignPurchaseRate(SubContracts localSubContract, int? modelSalesRateId)
        {
            localSubContract.PurchaseRate = modelSalesRateId.HasValue ?
                _purchaseRateRepository.GetById(modelSalesRateId.Value) : null;
            return localSubContract;
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete SubContractor by id {id}");
            ResultDto<bool> result = null;
            var localSubcontractor = _subContractRepository.GetById(id);
            if (localSubcontractor != null)
            {
                localSubcontractor = RemoveAssingationForPeople(localSubcontractor);
                localSubcontractor.PurchaseRate = null;
                var resultSave = _subContractRepository.DeleteSubContractor(localSubcontractor);
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

        private SubContracts RemoveAssingationForPeople(SubContracts localSubcontractor)
        {
            if (localSubcontractor.People != null && localSubcontractor.People.Any())
            {
                foreach (var people in localSubcontractor.People)
                {
                    people.SubcontractorResponsible = false;
                }
            }
            localSubcontractor.People = null;
            return localSubcontractor;
        }

        private IQueryable<SubContracts> OrderBy(IQueryable<SubContracts> query, SubContractFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }

        private SubContracts AssignPeople(SubContracts entity,
                                            IList<Entities.Tenant.People> peopleToAssign,
                                            IList<PeopleSelectableDto> peopleResponsable)
        {            
            foreach (var person in entity.People)
            {
                person.SubcontractorResponsible = false;
                _peopleRepository.UpdateOnContext(person);
            }
            entity.People?.Clear();
            if (peopleToAssign != null && peopleToAssign.Any())
            {               
                foreach (var personResponsable in peopleResponsable)
                {
                    var person = peopleToAssign.Single(x => x.Id == personResponsable.Id);
                    person.SubcontractorResponsible = true;
                }

                entity.People = entity.People ?? new List<Entities.Tenant.People>();
                entity.People = peopleToAssign;
            }
            return entity;
        }

        private bool CanAssignPeople(IList<PeopleSelectableDto> peopleSelected,
                                     out IList<Entities.Tenant.People> peopleToAssign, int? subContractId = null)
        {
            bool result = true;
            peopleToAssign = null;
            if (peopleSelected != null && peopleSelected.Any())
            {
                peopleToAssign = _peopleRepository.GetByPeopleIds(peopleSelected.Select(x => x.Id).ToList()).ToList();

                result = peopleToAssign.Any() && peopleToAssign.All(p => (p.SubcontractId == null || p.SubcontractId == subContractId)
                                                                         && p.CompanyId == null);
            }
            return result;
        }

        private SubContracts AssignKnowledges(SubContracts entity, IList<SubContractKnowledgeDto> knowledges)
        {
            entity.KnowledgeSubContracts?.Clear();
            if (knowledges != null && knowledges.Any())
            {
                entity.KnowledgeSubContracts = entity.KnowledgeSubContracts ?? new List<KnowledgeSubContracts>();
                var localKnowledges = _knowledgeRepository.FilterById(knowledges.Select(x => x.Id));
                foreach (var localKnowledge in localKnowledges)
                {
                    entity.KnowledgeSubContracts.Add(new KnowledgeSubContracts()
                    {
                        Knowledge = localKnowledge,
                        Maturity = knowledges.SingleOrDefault(x => x.Id == localKnowledge.Id)?.Priority ?? 0,
                    });
                }
            }
            return entity;
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get SubContract Key Value");
            var data = _subContractRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}