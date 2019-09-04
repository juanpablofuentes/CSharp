using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.DamagedEquipment;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.DamagedEquipment
{
    public class DamagedEquipmentService : BaseService, IDamagedEquipmentService
    {
        private readonly IDamagedEquipmentRepository _damagedEquipmentRepository;

        public DamagedEquipmentService(ILoggingService loggingService,
                                        IDamagedEquipmentRepository iDamagedEquipmentRepository) : base(loggingService)
        {
            _damagedEquipmentRepository = iDamagedEquipmentRepository ?? throw new ArgumentException($"{nameof(IDamagedEquipmentRepository)} is null");
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get DamagedEquipment Key Value");
            var data = _damagedEquipmentRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}