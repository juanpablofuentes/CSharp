using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.SOM.Web.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AssetsTransferViewModelExtensions
    {
        public static AssetsTransferDto ToDto(this AssetsTransferViewModel source)
        {
            AssetsTransferDto result = null;
            if (source != null)
            {
                result = new AssetsTransferDto()
                {
                    AssetsStatusId = source.AssetsStatusId,
                    SelectedAssetsId = source.AssetsId ?? new int[0],
                    SelectedSiteLocation = new KeyValuePair<int?, string>(source.Location.Value, source.Location.Text),
                    SelectedSiteUser = new KeyValuePair<int?, string>(source.User.Value, source.User.Text),
                    UserId = source.UserId
                };
            }
            return result;
        }
    }
}