using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.SOM.Web.Models.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderAssetViewModelExtensions
    {
        public static AssetsDetailViewModel ToAssetViewModel(this AssetForWorkOrderDetailsDto source)
        {
            AssetsDetailViewModel result = null;
            if (source != null)
            {
                result = new AssetsDetailViewModel();
                source.ToAssetsViewModel(result);
            }
            return result;
        }

        private static void ToAssetsViewModel(this AssetForWorkOrderDetailsDto source, AssetsDetailViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.SerialNumber = source.SerialNumber;
                target.AssetNumber = source.AssetNumber;
                target.StockNumber = source.StockNumber;
                target.Status = source.Status;
                target.Model = source.Model;
                target.Brand = source.Brand;
                target.Maintenance = source.Maintenance;
                target.MaintenanceStartDate = source.MaintenanceStartDate;
                target.MaintenanceEndDate = source.MaintenanceEndDate;
                target.StandardWarranty = source.StandardWarranty;
                target.StandardWarrantyStartDate = source.StandardWarrantyStartDate;
                target.StandardWarrantyEndDate = source.StandardWarrantyEndDate;
                target.ManufacturerWarranty = source.ManufacturerWarranty;
                target.ManufacturerWarrantyStartDate = source.ManufacturerWarrantyStartDate;
                target.ManufacturerWarrantyEndDate = source.ManufacturerWarrantyEndDate;
            }
        }

        public static AssetsDetailWorkOrderServicesViewModel ToAssetsDetailWorkOrderServicesViewModel(this AssetsDetailWorkOrderServicesDto source)
        {
            AssetsDetailWorkOrderServicesViewModel result = null;
            if (source != null)
            {
                result = new AssetsDetailWorkOrderServicesViewModel();
                source.ToAssetsDetailWorkOrderServicesViewModel(result);
            }
            return result;
        }

        private static void ToAssetsDetailWorkOrderServicesViewModel(this AssetsDetailWorkOrderServicesDto source, AssetsDetailWorkOrderServicesViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Observations = source.Observations;
                target.Repair = source.Repair;
                target.Service = source.Services.ToAssetsDetailServicesViewModelList().ToList();
            }
        }

        public static IList<AssetsDetailServicesViewModel> ToAssetsDetailServicesViewModelList(this IList<AssetsDetailServicesDto> source)
        {
            return source?.MapList(pk => pk.ToAssetsDetailServicesViewModel());
        }

        private static AssetsDetailServicesViewModel ToAssetsDetailServicesViewModel(this AssetsDetailServicesDto source)
        {
            AssetsDetailServicesViewModel result = null;
            if (source != null)
            {
                result = new AssetsDetailServicesViewModel();
                source.AssetsDetailServicesViewModel(result);
            }
            return result;
        }

        private static void AssetsDetailServicesViewModel(this AssetsDetailServicesDto source, AssetsDetailServicesViewModel target)
        {
            if (source != null && target != null)
            {
                target.ServiceId = source.ServiceId;
                target.Status = source.Status;
                target.FormState = source.FormState;
                target.DeliveryNumber = source.DeliveryNumber;
                target.ResponsibleName = source.ResponsibleName;
                target.ExtraFieldValues = source.ExtraFieldsValues;
            }
        }
    }
}