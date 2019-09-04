using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Constants.ClosureCode;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ClosingCode;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.ClosingCode;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.ClosureCode
{
    public class ClosingCodeService : BaseService, IClosingCodeService
    {
        private readonly IClosingCodeRepository _closingCodeRepository;

        public ClosingCodeService(ILoggingService logginingService, IClosingCodeRepository closingCodeRepository) : base(logginingService)
        {
            _closingCodeRepository = closingCodeRepository ?? throw new ArgumentNullException($"{nameof(IClosingCodeRepository)}");
        }


        public ResultDto<bool> CanDelete(int id)
        {
            var result = true;
            if (id != 0)
            {
                result = CanDeleteClosingCode(id);
            }
            return ProcessResult(result);
        }

        public ClosingCodesAnalysisDto GetAnalyzeClosingCodesById(int id)
        {
            var closingCode = _closingCodeRepository.GetByIdIncludeFathers(id);
            var closingCodesList = new List<ClosingCodes>();
            FillClosingCodesListOrdered(closingCode, closingCodesList);
            var dto = FillClosingCodesAnalysisFromCodeList(closingCodesList);
            return dto;
        }

        private void FillClosingCodesListOrdered(ClosingCodes closingCode, List<ClosingCodes> closingCodesList)
        {
            closingCodesList.Insert(0, closingCode);
            if (closingCode.ClosingCodesFather != null)
            {
                FillClosingCodesListOrdered(closingCode.ClosingCodesFather, closingCodesList);
            }
        }

        private ClosingCodesAnalysisDto FillClosingCodesAnalysisFromCodeList(List<ClosingCodes> closingCodesList)
        {
            var dto = new ClosingCodesAnalysisDto();
            for (int i = 0; i < closingCodesList.Count; i++)
            {
                dto.GetType().GetProperty($"{ClosureCodeConstants.ClosingCodeName}{i+1}")?.SetValue(dto, closingCodesList.ElementAt(i)?.Name);
                dto.GetType().GetProperty($"{ClosureCodeConstants.ClosingCodeDesc}{i+1}")?.SetValue(dto, closingCodesList.ElementAt(i)?.Description);
            }
            return dto;
        }

        private bool CanDeleteClosingCode(int id)
        {
            var result = true;
            var closingCode = _closingCodeRepository.GetById(id);
            if (closingCode.CanDelete())
            {
                var childs = closingCode.InverseClosingCodesFather.ToList();
                for (int i = 0; i < childs.Count && result; i++)
                {
                    result &= CanDeleteClosingCode(childs[i].Id);
                }
            }
            return result;
        }
    }
}