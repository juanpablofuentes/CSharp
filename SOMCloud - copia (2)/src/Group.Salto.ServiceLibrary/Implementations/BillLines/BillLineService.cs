using Group.Salto.ServiceLibrary.Common.Contracts.BillLines;
using Group.Salto.Log;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.BillLines;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.BillLines
{
    public class BillLineService : BaseService, IBillLineService
    {
        private readonly IBillLineRepository _billLineRepository;

        public BillLineService(ILoggingService logginingService,
                               IBillLineRepository billLineRepository) : base(logginingService)
        {
            _billLineRepository = billLineRepository ?? throw new ArgumentNullException($"{nameof(IBillLineRepository)} is null");
        }

        public ResultDto<IList<BillLinesDto>> GetAllById(int id)
        {
            var entity = _billLineRepository.GetAllByBillId(id);
            return ProcessResult(entity.ToListDto(), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }
    }
}