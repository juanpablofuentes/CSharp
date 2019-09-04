using Group.Salto.Common;
using Group.Salto.Common.Constants.People;
using Group.Salto.Entities;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.PeoplePermissions;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Contracts.User;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.PeoplePermissions;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Group.Salto.ServiceLibrary.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Extensions;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.ServiceLibrary.Common.Contracts;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class PeopleAdapter : BaseService, IPeopleAdapter
    {
        private readonly IPeopleService _peopleService;
        private readonly IUserConfigurationService _userConfigurationService;
        private readonly IAccessService _accessService;
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;
        private readonly IPermissionsService _permissionsService;
        private readonly IPeoplePermissionsService _peoplePermissionsService;
        private readonly ITranslationService _translationService;

        public PeopleAdapter(ILoggingService logginingService,
                            IPeopleService peopleService,
                            IUserConfigurationService userConfigurationService,
                            IAccessService accessService,
                            IIdentityService identityService,
                            IUserService userService,
                            IPermissionsService permissionsService,
                            IPeoplePermissionsService peoplePermissionsService,
                            ITranslationService translationService)
            : base(logginingService)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null ");
            _userConfigurationService = userConfigurationService ?? throw new ArgumentNullException($"{nameof(IUserConfigurationService)} is null ");
            _accessService = accessService ?? throw new ArgumentNullException($"{nameof(IAccessService)} is null ");
            _identityService = identityService ?? throw new ArgumentNullException($"{nameof(IIdentityService)} is null ");
            _userService = userService ?? throw new ArgumentNullException($"{nameof(IUserService)} is null ");
            _permissionsService = permissionsService ?? throw new ArgumentNullException($"{nameof(IPermissionsService)} is null ");
            _peoplePermissionsService = peoplePermissionsService ?? throw new ArgumentNullException($"{nameof(IPeoplePermissionsService)} is null ");
            _translationService = translationService ?? throw new ArgumentNullException($"{nameof(ITranslationService)} is null ");
        }

        public IEnumerable<PeopleListDto> GetList(PeopleFilterDto filter, Guid tenantId)
        {
            ResultDto<IList<PeopleListDto>> peopleList = _peopleService.GetListFiltered(filter);
            ResultDto<IList<PeopleListDto>> userList = _userService.GetAllFilteredByTenant(filter, tenantId);

            List<PeopleListDto> results = peopleList.Data.Join(userList.Data,
              P => P.UserConfigurationId,
              U => U.UserConfigurationId,
              (people, user) => new PeopleListDto { Id = people.Id, Dni = people.Dni, Name = people.Name, FisrtSurname = people.FisrtSurname, SecondSurname = people.SecondSurname, Email = people.Email, Telephone = people.Telephone, IsClientWorker = people.IsClientWorker, IsActive = people.IsActive, UserId = user.UserId, UserName = user.UserName, UserConfigurationId = people.UserConfigurationId }
            ).ToList();

            results.AddRange(peopleList.Data.Except(results, new PeopleComparer()));

            var userlistExcept = userList.Data.Except(results, new PeopleComparer());
            var peopleListVoid = _peopleService.GetByIds(userlistExcept.Where(x => x.UserConfigurationId.HasValue).Select(x => x.UserConfigurationId.Value).ToArray(), filter);

            List<PeopleListDto> userListWithPeopleData = peopleListVoid.Data.Join(userlistExcept,
              P => P.UserConfigurationId,
              U => U.UserConfigurationId,
              (people, user) => new PeopleListDto { Id = people.Id, Dni = people.Dni, Name = people.Name, FisrtSurname = people.FisrtSurname, SecondSurname = people.SecondSurname, Email = people.Email, Telephone = people.Telephone, IsClientWorker = people.IsClientWorker, IsActive = people.IsActive, UserId = user.UserId, UserName = user.UserName, UserConfigurationId = people.UserConfigurationId }
            ).ToList();

            results.AddRange(userListWithPeopleData);
            results = OrderBy(results, filter).ToList();

            return results;
        }

        public FileContentDto ExportToExcel(PeopleFilterDto filter, Guid tenantId)
        {
            IEnumerable<PeopleListDto> result = GetList(filter, tenantId);
            if (!filter.ExportAllToExcel)
            { 
                result = GetPageIEnumerable(result, filter).ToList();
            }
            byte[] file = null;
            if (result != null && result.Count() > 0)
            {
                List<string> columns = new List<string>() { _translationService.GetTranslationText("Name"), _translationService.GetTranslationText("FisrtSurname"), _translationService.GetTranslationText("SecondSurname"), _translationService.GetTranslationText("Dni"), _translationService.GetTranslationText("Email"), _translationService.GetTranslationText("Telephone"), _translationService.GetTranslationText("IsClientWorker"), _translationService.GetTranslationText("IsActive"), _translationService.GetTranslationText("UserName") };
                file = base.ExportToExcel(result.ToExcelListDto(), columns);
            }

            FileContentDto fileContent = new FileContentDto()
            {
                FileName = $"{DateTime.Now.ToString("yyyyMMdd")}_peopleList.xlsx",
                FileBytes = file
            };

            return fileContent;
        }

        public List<PeopleListDto> OrderBy(List<PeopleListDto> list, PeopleFilterDto filter)
        {
            IQueryable<PeopleListDto> query = list.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.FisrtSurname);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.SecondSurname);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Dni);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Telephone);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Email);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.IsActive);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.IsClientWorker);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.UserName);
            return query.ToList();
        }

        public async Task<ResultDto<GlobalPeopleDto>> CreatePeople(GlobalPeopleDto globalPeople)
        {
            ErrorsDto errors = new ErrorsDto();

            ResultDto<UserConfigurationsDto> userConfigurationsResult = null;
            ResultDto<PeopleDto> peopleCreate = null;

            Users userExist = await _identityService.FindByUserName(globalPeople.AccessUserDto.UserName);

            if (userExist == null)
            {
                var validations = await _accessService.ValidateAccessUser(globalPeople.AccessUserDto, userExist);
                if (validations.Any())
                {
                    errors.AddRangeError(validations);
                }
                else
                {
                    userConfigurationsResult = _userConfigurationService.CreateUserConfiguration();
                    globalPeople.AccessUserDto.UserConfigurationId = userConfigurationsResult.Data.Id;
                    globalPeople.PeopleDto.UserConfigurationId = userConfigurationsResult.Data.Id;

                    peopleCreate = _peopleService.CreatePeople(globalPeople);
                    SetPeopleError<PeopleDto>(peopleCreate, errors);

                    if (errors.Errors == null && globalPeople.AccessUserDto.AccessUserWithData())
                    {
                        await _accessService.CreateNewUser(globalPeople, errors);
                    }
                }
            }
            else
            {
                errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = PeopleConstants.PeopleAlreadyExist });
            }

            ResultDto<GlobalPeopleDto> result = new ResultDto<GlobalPeopleDto>()
            {
                Errors = (errors != null && errors.Errors != null && errors.Errors.Count > 0) ? errors : null,
                Data = new GlobalPeopleDto
                {
                    PeopleDto = (peopleCreate != null && peopleCreate.Data != null) ? peopleCreate.Data : new PeopleDto(),
                }
            };

            return result;
        }

        public async Task<ResultDto<GlobalPeopleDto>> UpdatePeople(GlobalPeopleDto globalPeople)
        {
            ErrorsDto errors = new ErrorsDto();
            ResultDto<PeopleDto> updatePeopleResult = null;

            updatePeopleResult = _peopleService.UpdatePeople(globalPeople);
            SetPeopleError<PeopleDto>(updatePeopleResult, errors);
            
            await _accessService.SaveAccessUser(globalPeople, errors);

            ResultDto<GlobalPeopleDto> result = new ResultDto<GlobalPeopleDto>()
            {
                Errors = (errors != null && errors.Errors != null && errors.Errors.Count > 0) ? errors : null,
                Data = new GlobalPeopleDto
                {
                    PeopleDto = (updatePeopleResult != null && updatePeopleResult.Data != null) ? updatePeopleResult.Data : new PeopleDto(),
                }
            };

            return result;
        }

        public ResultDto<bool> DeletePeople(int Id)
        {
            ErrorsDto errors = new ErrorsDto();

            ResultDto<PeopleDto> deleteResult = _peopleService.DeletePeople(Id);
            SetPeopleError<PeopleDto>(deleteResult, errors);

            if (errors.Errors == null)
            {
                ResultDto<bool> resultDeleteUser = _userService.DeleteUser(deleteResult.Data.UserConfigurationId.Value);
                SetPeopleError<bool>(resultDeleteUser, errors);
            }

            ResultDto<bool> result = new ResultDto<bool>()
            {
                Errors = (errors != null && errors.Errors != null && errors.Errors.Count > 0) ? errors : null,
                Data = !(errors != null && errors.Errors != null && errors.Errors.Count > 0)
            };

            return result;
        }

        private void SetPeopleError<T>(ResultDto<T> result, ErrorsDto errors)
        {
            if (result.Errors != null)
            {
                foreach (var e in result.Errors.Errors)
                {
                    errors.AddError(new ErrorDto() { ErrorType = e.ErrorType, ErrorMessageKey = e.ErrorMessageKey });
                }
            }
        }

        public ResultDto<List<MultiSelectItemDto>> GetPermissionList(int? peopleId)
        {
            LogginingService.LogInfo($"GetPermisionList for people {peopleId}");
            IEnumerable<PermissionsDto> permisions = _permissionsService.GetAllKeyValues();
            IList<PeoplePermissionsDto> peoplePermisions = new List<PeoplePermissionsDto>();

            if (peopleId.HasValue && peopleId.Value != 0)
                peoplePermisions = _peoplePermissionsService.GetByPeopleId(peopleId.Value);

            List<MultiSelectItemDto> multiSelectItemDto = new List<MultiSelectItemDto>();
            foreach (PermissionsDto permision in permisions)
            {
                bool isCheck = peoplePermisions.Any(x => x.PermissionId == permision.Id);

                multiSelectItemDto.Add(new MultiSelectItemDto()
                {
                    LabelName = permision.Name,
                    Value = permision.Id.ToString(),
                    IsChecked = isCheck
                });
            }

            return ProcessResult(multiSelectItemDto, multiSelectItemDto != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }
    }
}