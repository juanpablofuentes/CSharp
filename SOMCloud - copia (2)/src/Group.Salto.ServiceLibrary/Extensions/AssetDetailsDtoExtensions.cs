using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class AssetDetailsDtoExtensions
    {
        public static AssetDetailsDto ToPartialDetailsDto(this Locations source)
        {
            AssetDetailsDto result = null;
            if (source != null)
            {
                result = new AssetDetailsDto
                {
                    SelectedSiteLocation = new KeyValuePair<int, string>(source.Id, source.Name),
                    SiteClient = new KeyValuePair<int?, string>(source.LocationsFinalClients.Select(x=>x.FinalClientId).SingleOrDefault(), source.LocationsFinalClients.Select(x=>x.FinalClient.Name).SingleOrDefault()),
                };               
            }
            return result;
        }

        public static AssetDetailsDto ToDetailsDto(this Assets source)
        {
            AssetDetailsDto result = null;
            if (source != null)
            {
                var client = source.LocationClient?
                                   .LocationsFinalClients?
                                   .Select(x => x.FinalClient)
                                   .FirstOrDefault();
                result = new AssetDetailsDto
                {
                    Id = source.Id,
                    SerialNumber = source.SerialNumber,
                    StockNumber = source.StockNumber,
                    AssetNumber = source.AssetNumber,
                    Observations = source.Observations,
                    Warranty = source.Guarantee.ToDetailsDto(),
                    SelectedBrand = new KeyValuePair<int?, string>(source.Model?.BrandId, source.Model?.Brand?.Name),
                    SelectedModel = new KeyValuePair <int?,string> (source.ModelId, source.Model?.Name),
                    SelectedSubFamily = new KeyValuePair <int?,string> (source.SubFamilyId, source.SubFamily?.Nom),
                    SelectedFamily = new KeyValuePair <int?,string> (source.SubFamily?.FamilyId, source.SubFamily?.Family?.Name),
                    SelectedSiteLocation = new KeyValuePair<int, string>(source.LocationClientId, source.LocationClient?.Name),
                    SelectedSiteUser = new KeyValuePair <int?,string> (source.UserId, source.User?.Name),                   
                    SiteClient = new KeyValuePair<int?, string>(client?.Id, client?.Name),
                    SelectedStatus = source.AssetStatusId                    
                };
                result.HiredServices = source.AssetsHiredServices?
                                             .Where(x => x.AssetId == source.Id)
                                             .Select(x => x.HiredService?.ToDto()).ToList();
                result.SelectedContract = source.AssetsContracts?
                                                .Where(x => x.AssetsId == source.Id)
                                                .Select(x => x.ContractsId)
                                                .FirstOrDefault();
                result.Audits = source.AssetsAudit?.ToListDto().ToList();
            }
            return result;
        }

        public static AssetForWorkOrderDetailsDto ToForWorkOrderDetailsDto(this Assets source)
        {
            AssetForWorkOrderDetailsDto result = null;
            if (source != null)
            {
                result = new AssetForWorkOrderDetailsDto
                {
                    Id = source.Id,
                    SerialNumber = source.SerialNumber,
                    StockNumber = source.StockNumber,
                    AssetNumber = source.AssetNumber,
                    Status = source.AssetStatus.Name,
                    Model = source.Model.Name,
                    Brand = source.Model.Brand.Name,
                    Maintenance = source.Guarantee.Armored,
                    MaintenanceStartDate = source.Guarantee.BlnStartDate.ToString(),
                    MaintenanceEndDate = source.Guarantee.BlnEndDate.ToString(),
                    StandardWarranty = source.Guarantee.Standard,
                    StandardWarrantyStartDate = source.Guarantee.StdStartDate.ToString(),
                    StandardWarrantyEndDate = source.Guarantee.StdEndDate.ToString(),
                    ManufacturerWarranty = source.Guarantee.Provider,
                    ManufacturerWarrantyStartDate = source.Guarantee.ProStartDate.ToString(),
                    ManufacturerWarrantyEndDate = source.Guarantee.ProEndDate.ToString()
                };
            }
            return result;
        }

        public static Assets ToEntity(this AssetDetailsDto source)
        {
            Assets result = null;
            if (source != null)
            {
                result = new Assets()
                {
                    SerialNumber = source.SerialNumber,
                    StockNumber = source.StockNumber,
                    AssetNumber = source.AssetNumber,
                    Observations = source.Observations,
                    AssetStatusId = source.SelectedStatus,
                    LocationClientId = source.SelectedSiteLocation.Key,
                    ModelId = source.SelectedModel.Key,
                    SubFamilyId = source.SelectedSubFamily.Key,
                    UserId = source.SelectedSiteUser.Key,                   
                    Guarantee = source.Warranty.ToEntity()
                };
            }
            return result;
        }

        public static Assets Update(this Assets target, AssetDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.SerialNumber = source.SerialNumber;
                target.StockNumber = source.StockNumber;
                target.AssetNumber = source.AssetNumber;
                target.Observations = source.Observations;              
                target.Guarantee.Update(source.Warranty);
            }
            return target;
        }

        public static AssetsLocationsDto ToAssetsLocationsDto(this Assets source)
        {
            AssetsLocationsDto result = null;
            if (source != null)
            {
                result = new AssetsLocationsDto
                {
                    Id = source.Id,
                    LocationClientId = source.LocationClientId,
                    LocationName = source.LocationClient?.Name,
                    FinalClientId = (source.LocationClient!= null && source.LocationClient.LocationsFinalClients != null && source.LocationClient.LocationsFinalClients.Any()) ? source.LocationClient.LocationsFinalClients.FirstOrDefault().FinalClientId : 0,
                    FinalClientName = source.LocationClient?.LocationsFinalClients?.FirstOrDefault()?.FinalClient?.Name,
                    SiteUserId = source.UserId.HasValue ? source.UserId.Value : 0,
                    SiteUserName = $"{source.User?.Name} {source.User?.FirstSurname} {source.User?.SecondSurname}"
                };
            }
            return result;
        }
    }
}