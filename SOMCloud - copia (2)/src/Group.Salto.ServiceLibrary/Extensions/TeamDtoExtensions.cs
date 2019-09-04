using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class AssetDtoExtensions
    {
        public static AssetDto ToDto(this Assets dbModel)
        {
            var dto = new AssetDto
            {
                Id = dbModel.Id,
                Location = dbModel.Location?.ToDto(),
                Name = dbModel.Name,
                Observations = dbModel.Observations,
                LocationId = dbModel.LocationId,
                TeamNumber = dbModel.AssetNumber,
                Model = dbModel.Model?.ToDto(),
                SerialNumber = dbModel.SerialNumber,
                AssetStatusId = dbModel.AssetStatusId,
                Guarantee = dbModel.Guarantee?.ToDto(),
                GuaranteeId = dbModel.GuaranteeId,
                LocationClientId = dbModel.LocationClientId,
                ModelId = dbModel.ModelId,
                StockNumber = dbModel.StockNumber,
                SubFamilyId = dbModel.SubFamilyId,
                UsageId = dbModel.UsageId,
                UserId = dbModel.UserId,
                WorkOrders = dbModel.WorkOrders?.ToWorkOrderEquipmentDto(),
                AssetStatus = dbModel.AssetStatus?.Name,
                Subfamily = dbModel.SubFamily?.Nom,
                Family = dbModel.SubFamily?.Family?.Name
            };

            return dto;
        }
    }
}