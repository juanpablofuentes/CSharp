using Group.Salto.ServiceLibrary.Common.Mobility.Dto.BasicInfo;

namespace Group.Salto.ServiceLibrary.Common.Contracts.BasicInfo
{
    public interface IBasicInfoService
    {
        BasicInfoDto GetAppBasicInfo(int peopleIdInt);
        string GetAppMinVersion();
    }
}
