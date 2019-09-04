using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Postcondition;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Postconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.PostconditionsTypes;
using Group.Salto.ServiceLibrary.Common.Dtos.Preconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Group.Salto.ServiceLibrary.Implementations.Postconditions
{
    public class PostconditionsService : BaseService, IPostconditionsService
    {
        private readonly IPostconditionsCollectionRepository _postconditionsCollectionRepository;
        private readonly IPreconditionTypesService _preconditionTypesService;
        private readonly IPostconditionTypesService _postconditionTypesService;
        private readonly IPostconditionServiceFactory _postconditionServicesFactory;
        private readonly IPostconditionsRepository _postconditionsRepository;

        public PostconditionsService(ILoggingService logginingService,
                                IPreconditionTypesService preconditionTypesService,
                                IPostconditionTypesService postconditionTypesService,
                                IPostconditionServiceFactory postconditionServicesFactory,
                                IPostconditionsRepository postconditionsRepository,
                                IPostconditionsCollectionRepository postconditionsCollectionRepository) : base(logginingService)
        {
            _postconditionsCollectionRepository = postconditionsCollectionRepository ?? throw new ArgumentNullException(nameof(IPostconditionsCollectionRepository));
            _preconditionTypesService = preconditionTypesService ?? throw new ArgumentNullException(nameof(IPreconditionTypesService));
            _postconditionTypesService = postconditionTypesService ?? throw new ArgumentNullException(nameof(IPostconditionTypesService));
            _postconditionServicesFactory = postconditionServicesFactory ?? throw new ArgumentNullException($"{nameof(IPostconditionServiceFactory)} is null");
            _postconditionsRepository = postconditionsRepository ?? throw new ArgumentNullException($"{nameof(IPostconditionsRepository)} is null");
        }

        public ResultDto<IList<PostconditionCollectionDto>> GetAllByTaskId(int id)
        {
            LogginingService.LogInfo($"Get all postconditions by taskid");
            var preconditionTypes = _preconditionTypesService.GetAll();
            var postconditionTypes = _postconditionTypesService.GetAll();

            //Todo 
            //Hauria de cridar al repository de postconditioncollection en lloc del postcondition?
            var postconditionCollectionsList = _postconditionsCollectionRepository.GetAllByTaskId(id).ToList();
            IList<PostconditionCollectionDto> result = new List<PostconditionCollectionDto>();
            foreach (var postconditionCollectionItem in postconditionCollectionsList)
            {
                PostconditionCollectionDto postconditionCollection = new PostconditionCollectionDto();
                postconditionCollection.TaskId = id;
                if (postconditionCollectionItem.Preconditions.Any())
                {
                    if (postconditionCollection.PreconditionsList == null)
                    {
                        postconditionCollection.PreconditionsList = new List<PreconditionsDto>();
                    }
                    postconditionCollection.PreconditionsList = postconditionCollectionItem.Preconditions.ToList().ToDto(preconditionTypes);
                }

                if (postconditionCollectionItem.Postconditions.Any())
                {
                    if (postconditionCollection.PostconditionsList == null)
                    {
                        postconditionCollection.PostconditionsList = new List<PostconditionsDto>();
                    }
                    postconditionCollection.PostconditionsList = postconditionCollectionItem.Postconditions.ToList().ToDto(postconditionTypes);
                }
                postconditionCollection.Id = postconditionCollectionItem.Id;
                result.Add(postconditionCollection);
            }
            return ProcessResult(result);
        }

        public IList<BaseNameIdDto<int>> GetPostconditionValues(string postconditionTypeName, FilterQueryParametersBase filterQueryParameters)
        {
            var languageId = filterQueryParameters.LanguageId;

            PostconditionsTypesDto postconditionType = _postconditionTypesService.GetPostconditionsTypeByName(postconditionTypeName);
            var query = _postconditionServicesFactory.GetQuery(postconditionType.Description);
            IList<BaseNameIdDto<int>> values = null;

            if (postconditionType.Description == nameof(PostconditionActionTypeEnum.TipusOTN1))
            {
                WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    WOTypeIdsToMatch = new List<int?>(),
                };
                wOTypeQueryParameters.WOTypeIdsToMatch.Add(null);
                values = query.GetAllKeyValues(wOTypeQueryParameters);
            }
            else
            {
                values = query.GetAllKeyValues(filterQueryParameters);
            }

            return values;
        }

        public IList<BaseNameIdDto<int>> GetTypeOtnValues(int id, FilterQueryParametersBase filterQueryParameters)
        {
            var languageId = filterQueryParameters.LanguageId;
            var allPostconditionType = _postconditionTypesService.GetAll().ToList();
            var otnType = allPostconditionType.Where(x => x.Description == nameof(PostconditionActionTypeEnum.TipusOTN1)).FirstOrDefault();

            PostconditionsTypesDto postconditionType = _postconditionTypesService.GetPostconditionsTypeByName(otnType.Name);
            var query = _postconditionServicesFactory.GetQuery(postconditionType.Description);
            IList<BaseNameIdDto<int>> values = null;

            WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
            {
                LanguageId = filterQueryParameters.LanguageId,
                WOTypeIdsToMatch = new List<int?>(),
            };
            wOTypeQueryParameters.WOTypeIdsToMatch.Add(id);
            values = query.GetAllKeyValues(wOTypeQueryParameters);

            return values;
        }

        public PostconditionsDto Update(PostconditionsDto postcondition)
        {
            LogginingService.LogInfo($"Update Postcondition");
            var allPostconditionType = _postconditionTypesService.GetAll().ToList();

            var postconditionType = allPostconditionType.Where(x => x.Name == postcondition.NameFieldModel).FirstOrDefault();

            var localModel = _postconditionsRepository.GetById(postcondition.Id);
            localModel.ToEntity(postcondition, postconditionType);

            var result = _postconditionsRepository.UpdatePostconditions(localModel);
            return result.Entity.ToDto(allPostconditionType);
        }

        public PostconditionsDto Create(PostconditionsDto postcondition)
        {
            LogginingService.LogInfo($"Create new Postcondition");

            var allPostconditionType = _postconditionTypesService.GetAll().ToList();
            var postconditionType = allPostconditionType.Where(x => x.Name == postcondition.NameFieldModel).FirstOrDefault();

            var localModel = _postconditionsRepository.GetById(postcondition.Id);
            var entity = localModel.ToEntity(postcondition, postconditionType);

            var result = _postconditionsRepository.CreatePostconditions(entity);
            return result.Entity.ToDto(allPostconditionType);
        }

        public PostconditionCollectionDto CreatePostconditionCollection(int taskId)
        {
            LogginingService.LogInfo($"Create new Postcondition Collection");

            PostconditionCollections postconditionCollection = new PostconditionCollections()
            {
                TaskId = taskId,
            };

            var localModel = _postconditionsCollectionRepository.CreatePostconditionCollection(postconditionCollection);

            PostconditionCollectionDto postconditionCollectionDto = new PostconditionCollectionDto()
            {
                Id = localModel.Entity.Id,
                TaskId = localModel.Entity.TaskId.HasValue ? localModel.Entity.TaskId.Value : 0,
            };

            return postconditionCollectionDto;
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete new Postcondition");
            ResultDto<bool> result = null;
            var localModel = _postconditionsRepository.GetById(id);
            if (localModel != null)
            {
                var resultSave = _postconditionsRepository.DeletePostconditions(localModel);
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

        public ResultDto<bool> DeleteAllPostconditions(int postconditionCollectionId)
        {
            LogginingService.LogInfo($"Delete new Postcondition");
            ResultDto<bool> result = null;
            var postconditions = _postconditionsRepository.GetByPostconditionCollectionId(postconditionCollectionId);
            if (postconditions != null)
            {
                foreach (var postcondition in postconditions)
                {
                    var resultSave = _postconditionsRepository.DeletePostconditionsOnContext(postcondition);
                }
                var resultRangeSave = _postconditionsRepository.DeleteRangePostconditions(postconditions);
                result = ProcessResult(resultRangeSave.IsOk, resultRangeSave);
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

        public bool CanDeletePostconditionCollection(int id)
        {
            LogginingService.LogInfo($"Delete PostconditionCollection");

            var localModel = _postconditionsCollectionRepository.GetById(id);
            if (localModel.Postconditions.Count() == 0 && localModel.Preconditions.Count() == 0)
            {
                return true;
            }

            return false;
        }

        public PostconditionCollectionDto DeletePostconditionCollection(int id)
        {
            LogginingService.LogInfo($"Delete PostconditionCollection");

            var localModel = _postconditionsCollectionRepository.GetById(id);
            var preconditionTypes = _preconditionTypesService.GetAll();
            var postconditionTypes = _postconditionTypesService.GetAll();

            SaveResult<PostconditionCollections> result = null;
            if (localModel != null)
            {
                result = _postconditionsCollectionRepository.DeletePostconditionCollection(localModel);

            }
            PostconditionCollectionDto postconditionCollection = null;
            if (result.IsOk)
            {
                postconditionCollection = new PostconditionCollectionDto();
                if (result.Entity.Preconditions.Any())
                {
                    if (postconditionCollection.PreconditionsList == null)
                    {
                        postconditionCollection.PreconditionsList = new List<PreconditionsDto>();
                    }
                    postconditionCollection.PreconditionsList = result.Entity.Preconditions.ToList().ToDto(preconditionTypes);
                }

                if (result.Entity.Postconditions.Any())
                {
                    if (postconditionCollection.PostconditionsList == null)
                    {
                        postconditionCollection.PostconditionsList = new List<PostconditionsDto>();
                    }
                    postconditionCollection.PostconditionsList = result.Entity.Postconditions.ToList().ToDto(postconditionTypes);
                }
                postconditionCollection.Id = result.Entity.Id;
            }

            return postconditionCollection;
        }
    }
}