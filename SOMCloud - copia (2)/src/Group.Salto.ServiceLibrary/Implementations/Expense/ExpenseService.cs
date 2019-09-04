using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Constants.ExpenseTicket;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.AzureBlobStorage;
using Group.Salto.ServiceLibrary.Common.Contracts.Expense;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.ServiceLibrary.Common.Dtos.ExpenseTicket;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.ServiceLibrary.Implementations.Expense
{
    public class ExpenseService : BaseFilterService, IExpenseService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IExpenseTicketRepository _expenseTicketRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseTicketStatusRepository _expenseTicketStatusRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IWorkOrdersRepository _workOrdersRepository;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IExpenseTicketFileRepository _expenseTicketFileRepository;
        private readonly ISomFileRepository _somFileRepository;
        private readonly IConfiguration _configuration;

        public ExpenseService(ILoggingService logginingService,
                            IExpenseTypeRepository expenseTypeRepository,
                            IPaymentMethodRepository paymentMethodRepository,
                            IExpenseTicketRepository expenseTicketRepository,
                            IExpenseTicketStatusRepository expenseTicketStatusRepository,
                            IExpenseRepository expenseRepository,
                            IPeopleRepository peopleRepository,
                            IWorkOrdersRepository workOrdersRepository,
                            IAzureBlobStorageService azureBlobStorageService,
                            IExpenseTicketFileRepository expenseTicketFileRepository,
                            ISomFileRepository somFileRepository,
                            IConfiguration configuration,
                            IExpenseQueryFactory queryFact) : base(queryFact,logginingService)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _expenseTicketRepository = expenseTicketRepository;
            _expenseTicketStatusRepository = expenseTicketStatusRepository;
            _expenseRepository = expenseRepository;
            _peopleRepository = peopleRepository;
            _workOrdersRepository = workOrdersRepository;
            _azureBlobStorageService = azureBlobStorageService;
            _expenseTicketFileRepository = expenseTicketFileRepository;
            _somFileRepository = somFileRepository;
            _configuration = configuration;
        }

        public ResultDto<IList<ExpenseTicketDto>> GetAllFiltered(ExpenseTicketFilterDto filter)
        {
            var query = _expenseTicketRepository.GetAllWithIncludes();
            query = Filter(query, filter);
            query = Pagin(query, filter);
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            return ProcessResult<IList<ExpenseTicketDto>>(data.ToList());
        }

        public int CountId(ExpenseTicketFilterDto filter)
        {
            var query = _expenseTicketRepository.GetAllWithIncludes();
            query = Filter(query, filter);
            return query.Count();
        }
        private IQueryable<Entities.Tenant.ExpensesTickets> Pagin(IQueryable<Entities.Tenant.ExpensesTickets> data,ExpenseTicketFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, false, au => au.Id);
            query = query.Skip(filter.Size * (filter.Page - 1));
            query = query.Take(filter.Size);
            return query;
        }

        private IQueryable<Entities.Tenant.ExpensesTickets> Filter(IQueryable<Entities.Tenant.ExpensesTickets> data, ExpenseTicketFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.WhereIfNotDefault(filter.InitialDate, au => au.Date >= filter.InitialDate);
            query = query.WhereIfNotDefault(filter.FinalDate, au => au.ValidationDate <= filter.FinalDate);
            query = query.WhereIfNotDefault(filter.NamePeople, au => filter.NamePeople.Contains(au.People.Id));
            query = query.WhereIfNotDefault(filter.States, au => filter.States.Contains(au.ExpenseTicketStatusId));
            return query;
        }

        public ResultDto<IList<ExpenseTicketExtDto>> GetAllExpenseFiltered(ExpenseTicketFilterDto filter)
        {
            var query = _expenseTicketRepository.GetAllWithIncludes();
            query = Filter(query, filter);
            query = Pagin(query, filter);
            var data = query.ToList().MapList(c => c.ToExtDto()).AsQueryable();
            return ProcessResult<IList<ExpenseTicketExtDto>>(data.ToList());
        }

        public IList<BaseNameIdDto<Guid>> GetExpenseTicketStatus()
        {
            var data = _expenseTicketStatusRepository.GetAllStatus();
            return data.Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<ExpenseTicketExtDto> GetByIdWithExpenseAndFile(int Id)
        {
            LogginingService.LogInfo($"Get By Id Expense");
            var data = _expenseTicketRepository.GetByIdIncludeSomFile(Id);
            return ProcessResult(data.ToExtDto());
        }

        public IEnumerable<ExpenseTicketExtDto> GetExpensesFromAppUser(int peopleId)
        {
            var currentPeople = _peopleRepository.GetByConfigId(peopleId);
            if (currentPeople != null)
            {
                peopleId = currentPeople.Id;
            }
            var expensesTiket = _expenseTicketRepository.GetByPeopleId(peopleId).Include(x => x.Expenses)
                                .OrderByDescending(x => x.Date).Where(e => e.ValidationDate == null ||
                                (e.ValidationDate != null && e.ValidationDate.Value.AddDays(60) > DateTime.UtcNow));

            var listTickets = expensesTiket.ToExtDto();
            return listTickets;
        }

        public ResultDto<int> AddExpense(ExpenseTicketExtDto expenseTicketExtDto, int peopleIdInt)
        {
            var process = CreateExpense(expenseTicketExtDto, peopleIdInt);
            var result = new ResultDto<int>()
            {
                Data = process.Data?.Id ?? 0,
                Errors = process.Errors
            };
            return result;
        }

        public ResultDto<ExpenseTicketExtDto> CreateExpense(ExpenseTicketExtDto expenseTicketExtDto, int peopleIdInt)
        {
            LogginingService.LogInfo($"Create expense from peopleId: {peopleIdInt}");

            var currentPeople = _peopleRepository.GetByConfigIdIncludingCompany(peopleIdInt);
            //TODO Create enum expense status, split in two functions
            var expenseDefaultStatus = _expenseTicketStatusRepository.GetAll().FirstOrDefault(x => x.Description.ToLower().Contains("pending"));
            var expenseTicket = new ExpensesTickets
            {
                PeopleId = peopleIdInt,
                Date = expenseTicketExtDto.Date,
                UpdateDate = DateTime.UtcNow,
                People = currentPeople,
                ExpenseTicketStatusId = expenseDefaultStatus?.Id,
                UniqueId = Guid.NewGuid().ToString()
            };

            if (expenseTicketExtDto.WorkOrderId.HasValue)
            {
                var wOder = _workOrdersRepository.GetById(expenseTicketExtDto.WorkOrderId.Value);
                expenseTicket.WorkOrder = wOder;
            }

            var paymentMethod = _paymentMethodRepository.GetById(expenseTicketExtDto.PaymentMethodId);
            var expenseType = _expenseTypeRepository.GetById(expenseTicketExtDto.ExpenseTypeId);
            double expenseFactor = 1;

            if (expenseType.Unit.Contains(ExpenseUnitTypeEnum.Kilometer.ToString()))
            {
                expenseFactor = currentPeople?.CostKm ?? currentPeople?.Company?.CostKm ?? 1;
            }
            var expense = new Expenses
            {
                Date = expenseTicketExtDto.Date,
                Amount = expenseTicketExtDto.Amount,
                UpdateDate = DateTime.UtcNow,
                Description = expenseTicketExtDto.Description,
                ExpenseTypeId = expenseTicketExtDto.ExpenseTypeId,
                PaymentMethod = paymentMethod,
                PaymentMethodId = paymentMethod.Id,
                Factor = expenseFactor
            };

            expenseTicket.Expenses = new List<Expenses>
            {
                expense
            };

            var resultRepository = _expenseTicketRepository.CreateExpensesTicket(expenseTicket);
            var process = ProcessResult(resultRepository.Entity?.ToExtDto(), resultRepository);
            return process;
        }

        public ResultDto<ExpenseTicketExtDto> UpdateExpense(ExpenseTicketExtDto source)
        {
            LogginingService.LogInfo($"Update Expense");
            ResultDto<ExpenseTicketExtDto> result = null;
            var findExpense = _expenseTicketRepository.GetByIdIncludeSomFile(source.Id);
            if (findExpense != null)
            {
                var updatedExpense = findExpense.Update(source);               
                var resultRepository = _expenseTicketRepository.UpdateExpensesTicket(updatedExpense);
                result = ProcessResult(resultRepository.Entity?.ToExtDto(), resultRepository);
            }

            return result ?? new ResultDto<ExpenseTicketExtDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteExpense(int id)
        {
            LogginingService.LogInfo($"Delete expense by id {id}");
            ResultDto<bool> result = null;
            
            var expense = _expenseRepository.GetByExpenseTicketId(id);
            if (expense != null && expense.FirstOrDefault()!=null)
            {
                var resultDelete = _expenseRepository.DeleteExpense(expense.FirstOrDefault());
            }
            
            var expenseTicket = _expenseTicketRepository.GetByIdIncludeSomFile(id);
            var somFileDto = new SomFileDto();
            if (expenseTicket.ExpensesTicketFile.FirstOrDefault() != null) { 
                somFileDto = expenseTicket.ExpensesTicketFile.FirstOrDefault().SomFile.ToDto();
            }
             
            if (expenseTicket != null)
            {
                if (expenseTicket.ExpensesTicketFile.FirstOrDefault() != null)
                {
                    var deleteBlob = _azureBlobStorageService.DeleteFileToBlobStorage(somFileDto);
                    var somFile = _somFileRepository.DeleteSomFile(expenseTicket.ExpensesTicketFile.FirstOrDefault().SomFile);
                    var expenseTicketFile = _expenseTicketFileRepository.DeleteExpenseTicketFile(expenseTicket.ExpensesTicketFile.FirstOrDefault());
                }
               
                var resultSave = _expenseTicketRepository.DeleteExpensesTicket(expenseTicket);
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

        public ResultDto<ExpenseTicketExtDto> ValidateExpense(ExpenseTicketExtDto expenseData,Guid? nextStatus)
        {
            var statesExpense = _expenseTicketStatusRepository.GetAll();
            var dataExpense = _expenseTicketRepository.GetByIdIncludeSomFile(expenseData.Id);
            var currentPeople = _peopleRepository.GetByConfigId(expenseData.validatorPeopleId);
            foreach (var state in statesExpense)
            {
                if (expenseData.Status == state.Id)
                {
                    if (state.Description.Contains(ExpenseTicketConstants.ExpenseTicketPending))
                    {
                        if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketRejected)).Id)
                        {
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketRejected, statesExpense);
                        }
                        else if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketAccepted)).Id)
                        {
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketAccepted, statesExpense);
                        }
                        else if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketPaid)).Id)
                        {
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketPaid, statesExpense); ;
                        }
                        else if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketFinished)).Id)
                        {
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketFinished, statesExpense);
                        }
                        else if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketEscaled)).Id)
                        {
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketEscaled, statesExpense);
                        }
                    }
                    else if (state.Description.Contains(ExpenseTicketConstants.ExpenseTicketAccepted))
                    {
                        if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketPaid)).Id)
                        {
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketPaid, statesExpense);
                        }
                        else if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketFinished)).Id)
                        {
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketFinished, statesExpense);
                        }
                    }
                    else if (state.Description.Contains(ExpenseTicketConstants.ExpenseTicketEscaled))
                    {
                        if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketAccepted)).Id)
                        {
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketAccepted, statesExpense);
                        }
                        else if (nextStatus == statesExpense.FirstOrDefault(x => x.Description.Contains(ExpenseTicketConstants.ExpenseTicketRejected)).Id)
                        {                            
                            expenseData = AssignValidationValues(expenseData, ExpenseTicketConstants.ExpenseTicketRejected, statesExpense);
                        }
                    }
                }
            }
            expenseData.PeopleValidator = currentPeople.ToDto();
            var updatedExpense = dataExpense.Update(expenseData);
            var resultRepository = _expenseTicketRepository.UpdateExpensesTicket(updatedExpense);
            var process = ProcessResult(resultRepository.Entity?.ToExtDto(), resultRepository);
            return process;           
        }

        public FileContentDto GetFileFromExpense(int id)
        {
            var fileContentDto = new FileContentDto();
            var expenseTiket = _expenseTicketRepository.GetByIdIncludeSomFile(id);
            var somFile = expenseTiket?.ExpensesTicketFile?.FirstOrDefault()?.SomFile;
            if (somFile != null)
            {
                var expenseSettingFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderExpenses);
                var somDto = somFile.ToDto();
                somDto.Directory = $"{expenseSettingFolder}/{somDto.Directory}";
                fileContentDto = _azureBlobStorageService.GetFileFromSomFile(somDto);
            }
            return fileContentDto;
        }
        
        public ResultDto<bool> AddFileToExpense(RequestFileDto requestFileDto)
        {
            var result = new ResultDto<bool>
            {
                Data = false,
                Errors = new ErrorsDto{Errors = new List<ErrorDto>{new ErrorDto{ErrorMessageKey = "Not found", ErrorType = ErrorType.EntityNotExists}}}
            };

            var currentExpense = _expenseTicketRepository.GetById(requestFileDto.Id);
            if (currentExpense != null)
            {
                //TODO need refactor blob functionality
                var expenseSettingFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderExpenses);
                var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);
                var expenseFolder = currentExpense.UniqueId;
                
                var fileBlobDto = new SaveFileBlobDto
                {
                    Name = requestFileDto.FileName,
                    Container = container,
                    Directory = $"{expenseSettingFolder}/{expenseFolder}",
                    FileBytes = requestFileDto.FileBytes
                };
                var blobResult = _azureBlobStorageService.SaveFileToBlobStorage(fileBlobDto);

                if (blobResult)
                {
                    result = AddSomFileToExpenseTicket(requestFileDto, currentExpense, fileBlobDto, expenseFolder);
                }
                else
                {
                    result.Errors = new ErrorsDto
                    {
                        Errors = new List<ErrorDto>{new ErrorDto {ErrorMessageKey = "Error saving file to Blob", ErrorType = ErrorType.SaveChangesException}}
                    };
                }
            }

            return result;
        }

        public ExpensesBasicFiltersInfoDto GetBasicFiltersInfo()
        {
            var expenseTypes = _expenseTypeRepository.GetAll().ToDto();
            var paymentTypes = _paymentMethodRepository.GetAll().ToDto();
            var expenseStatus = _expenseTicketStatusRepository.GetAll().ToDto();

            var dto = new ExpensesBasicFiltersInfoDto
            {
                ExpenseTypes = expenseTypes,
                PaymentTypes = paymentTypes,
                ExpenseStatus = expenseStatus
            };
            return dto;
        }

        public IList<ExpenseTicketDto> CalculateAmount(IList<ExpenseTicketDto> source)
        {
            var results = new List<ExpenseTicketDto>();
            foreach (var item in source.ToList()) {
                if (item.TicketExpenses != null && item.TicketExpenses.Count() > 0)
                {
                    if (item.TicketExpenses.Select(x => x.ExpenseTypeId).FirstOrDefault() == 2)
                    {                        
                            item.TicketExpenses.FirstOrDefault().Amount = item.TicketExpenses.Select(x => x.Amount).FirstOrDefault() * (decimal)item.TicketExpenses.FirstOrDefault().Factor;
                            results.Add(item);                                                                                            
                    }
                    else
                    {
                        item.TicketExpenses.FirstOrDefault().Amount = item.TicketExpenses.Select(x => x.Amount * 1).FirstOrDefault();
                        results.Add(item);
                    }
                }
            }
            return results;
        }

        public Dictionary<Guid,string> GetDefaultStates()
        {
            var data = new Dictionary<Guid,string>();
            var states = _expenseTicketStatusRepository.GetAllStatus();
            foreach(var item in states)
            {
                if(String.Compare(item.Value,ExpenseTicketConstants.ExpenseTicketAccepted)== 0 || String.Compare(item.Value,ExpenseTicketConstants.ExpenseTicketEscaled) ==0|| String.Compare(item.Value,ExpenseTicketConstants.ExpenseTicketPending)==0)
                {
                    data.Add(item.Key,item.Value);
                }
            }
            return data;
        }

        public IList<BaseNameIdDto<int>> GetPaymentMethodKeyValues()
        {
            LogginingService.LogInfo($"Get Payment method Key Value");
            var data = _paymentMethodRepository.GetPaymentMethodKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetExpenseTypeKeyValues()
        {
            LogginingService.LogInfo($"Get Expense type Key Value");
            var data = _expenseTypeRepository.GetExpenseTypeKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        private ExpenseTicketExtDto AssignValidationValues(ExpenseTicketExtDto data,string constanStatus,IQueryable<ExpenseTicketStatus> states)
        {
            data.Status = states.FirstOrDefault(x => x.Description.Contains(constanStatus)).Id;
            data.ValidationDate = DateTime.UtcNow;            
            data.StatusExpense = constanStatus;
            return data;
        }

        private ResultDto<bool> AddSomFileToExpenseTicket(RequestFileDto requestFileDto, Entities.Tenant.ExpensesTickets currentExpense, SaveFileBlobDto fileBlobDto, string expenseFolder)
        {
            var hashVal = GetMd5FromByteArray(requestFileDto.FileBytes);
            currentExpense.ExpensesTicketFile?.Clear();
            currentExpense.ExpensesTicketFile = new List<ExpensesTicketFile>
            {
                new ExpensesTicketFile
                {
                    SomFile = new SomFiles
                    {
                        Container = fileBlobDto.Container,
                        Directory = expenseFolder,
                        Name = fileBlobDto.Name,
                        UpdateDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        ContentMd5 = hashVal
                    }
                }
            };
            var resultSave = _expenseTicketRepository.UpdateExpensesTicket(currentExpense);
            var process = ProcessResult(resultSave.Entity?.ToExtDto(), resultSave);
            var result = new ResultDto<bool>
            {
                Data = process.Data?.Id != null,
                Errors = process.Errors
            };
            return result;
        }

        private IQueryable<ExpenseTicketDto> OrderBy(IQueryable<ExpenseTicketDto> query, ExpenseTicketFilterDto filter)
        {
            LogginingService.LogInfo($"Order By expense ticket");
            query = query.MaybeSort(filter.OrderBy, !filter.Asc, au => au.Date);
            query = query.OrderByDescending(x => x.Date);
            return query;
        }

        private string GetMd5FromByteArray(byte[] byteArray)
        {
            var md5Check = System.Security.Cryptography.MD5.Create();
            md5Check.TransformBlock(byteArray, 0, byteArray.Length, null, 0);
            md5Check.TransformFinalBlock(new byte[0], 0, 0);
            var hashVal = Convert.ToBase64String(md5Check.Hash);
            return hashVal;
        }
    }
}