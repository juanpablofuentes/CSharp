using Group.Salto.ServiceLibrary.Common.Contracts.Billing;
using Group.Salto.Log;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Billing;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.ServiceLibrary.Common.Dtos.Bill;
using System.Linq;
using Group.Salto.Extensions;
using Group.Salto.ServiceLibrary.Common.Contracts.Bill;

namespace Group.Salto.ServiceLibrary.Implementations.Billing
{
    public class BillService : BaseService , IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillLineRepository _billLineRepository;
        public BillService(ILoggingService logginingService,
                           IBillLineRepository billLineRepository,
                           IBillRepository billRepository) : base(logginingService)
        {
            _billRepository = billRepository ?? throw new ArgumentNullException($"{nameof(IBillRepository)} is null");
            _billLineRepository = billLineRepository ?? throw new ArgumentNullException($"{nameof(IBillLineRepository)} is null");
        }

        public ResultDto<IList<BillDto>> GetAllById(int id)
        {
            var entity = _billRepository.GetAllByWorkOrderId(id);
            return ProcessResult(entity.ToListDto(), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<IList<BillInfoDto>> GetAllFiltered(BillFilterDto filter)
        {
            var query = _billRepository.GetAll();
            query = query.WhereIfNotDefault(filter.WorkOrderId, au => au.WorkorderId.ToString().Contains(filter.WorkOrderId.ToString()));
            query = Pagin(query, filter);
            query = Filter(query, filter);
            return ProcessResult(BillInfoDtoExtension.ToListDtoBill(query.ToList()));
        }

        public int CountId(BillFilterDto filter)
        {
            var query = _billRepository.GetAll();
            query = query.WhereIfNotDefault(filter.WorkOrderId, au => au.WorkorderId.ToString().Contains(filter.WorkOrderId.ToString()));
            query = Filter(query, filter);
            return query.Count();
        }

        private IQueryable<Entities.Tenant.Bill> Pagin(IQueryable<Entities.Tenant.Bill> data, BillFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, false, au => au.Id);
            query = query.Skip(filter.Size * (filter.Page - 1));
            query = query.Take(filter.Size);
            return query;
        }

        private IQueryable<Entities.Tenant.Bill> Filter(IQueryable<Entities.Tenant.Bill> data, BillFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.WhereIfNotDefault(filter.Status, au => au.Status.Equals(filter.Status));  
            query = query.WhereIfNotDefault(filter.ProjectSerie, au => au.Workorder.Project.Status.Contains(filter.ProjectSerie));
            query = query.WhereIfNotDefault(filter.ItemId, au => au.BillLine.ToString().Contains(filter.ItemId.ToString()));
            query = query.WhereIfNotDefault(filter.InternalIdentifier, au => au.Workorder.FinalClientId.ToString().Contains(filter.InternalIdentifier));
            query = query.WhereIfNotDefault(filter.Project, au => au.Workorder.Project.Name.Contains(filter.Project));
            query = query.Where(au => au.Date >= (filter.StartDate) && au.Date <= (filter.EndDate));
            return query;
        }

        public ResultDto<IList<BillDetailDto>> GetDetailById(int id)
        {
            var entity = _billLineRepository.GetAllByBillId(id);
            return ProcessResult(entity.ToListDtoBillDetail());
        }

        public ResultDto<BillInfoDto> GetBillById(int id)
        {
            var data = _billRepository.GetById(id);
            return ProcessResult(BillInfoDtoExtension.ToDtoBill(data));
        }
    }
}