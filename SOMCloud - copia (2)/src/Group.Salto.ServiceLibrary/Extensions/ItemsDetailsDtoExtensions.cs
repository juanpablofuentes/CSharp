using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ItemsDetailsDtoExtensions
    {
        public static ItemsDetailsDto ToDto(this Items source)
        {
            ItemsDetailsDto result = null;
            if (source != null)
            {
                result = new ItemsDetailsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    InDepot = source.InDepot,
                    IsBlocked = source.IsBlocked,
                    TrackSerialNumber = source.TrackBySerialNumber,
                    SyncErp = source.SyncErp,
                    InternalReference = source.InternalReference,
                    ERPReference = source.ErpReference,
                    Picture = source.Picture ?? new byte[0],
                    SelectedSubFamily = new KeyValuePair<int?, string>(source.SubFamiliesId, source.SubFamilies?.Nom),
                    SelectedFamily = new KeyValuePair<int?, string>(source.SubFamilies?.FamilyId, source.SubFamilies?.Family?.Name),
                    ItemsSerialNumbers = source.ItemsSerialNumber.ToList().ToListDto(),
                    ItemsTypeId = source.Type
                };
                result.ItemPointsRates = source.ItemsPointsRate?.ToList().ToDto() ?? new List<RateDto>();
                result.ItemPurchaseRates = source.ItemsPurchaseRate?.ToList().ToDto() ?? new List<RateDto>();
                result.ItemSalesRates = source.ItemsSalesRate?.ToList().ToDto() ?? new List<RateDto>();                
            }
            return result;
        }
        
        public static Items ToEntity(this ItemsDetailsDto source)
        {
            Items result = null;
            if (source != null)
            {
                result = new Items()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    InDepot = source.InDepot,
                    IsBlocked = source.IsBlocked,
                    TrackBySerialNumber = source.TrackSerialNumber,
                    SyncErp = source.SyncErp,
                    InternalReference = source.InternalReference,
                    ErpReference = source.ERPReference,
                    Picture = source.Picture ?? new byte[0],
                    SubFamiliesId = source.SelectedSubFamily.Key,
                    Type = source.ItemsTypeId                     
                };
            }

            return result;
        }

        public static Items Update(this Items target, ItemsDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.ErpReference = source.ERPReference;
                target.InternalReference = source.InternalReference;
                target.SyncErp = source.SyncErp;              
                target.InDepot = source.InDepot;              
                target.IsBlocked = source.IsBlocked;              
                target.TrackBySerialNumber = source.TrackSerialNumber;              
                target.Picture = source.Picture ?? target.Picture;
                target.SubFamiliesId = source.SelectedSubFamily.Key;
            }
            return target;
        }
    }
}