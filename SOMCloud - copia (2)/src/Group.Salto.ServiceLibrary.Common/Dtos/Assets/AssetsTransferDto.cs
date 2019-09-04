using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Assets
{
    public class AssetsTransferDto
    {
        public int[] SelectedAssetsId { get; set; }
        public int? AssetsStatusId { get; set; }
        public KeyValuePair<int?, string> SelectedSiteUser { get; set; }
        public KeyValuePair<int?,string> SelectedSiteLocation { get; set; }
        public int UserId { get; set; }
    }
}