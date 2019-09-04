using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Rols;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Rol
{
    public interface IRolService
    {
        ResultDto<IEnumerable<RolDto>> GetAll();
        Task<ResultDto<RolDto>> GetById(int Id);
        ResultDto<IList<RolListDto>> GetListFiltered(RolFilterDto filter);
        Task<ResultDto<RolDto>> CreateRol(RolDto rol);
        Task<ResultDto<RolDto>> UpdateRol(RolDto rol);
        Task<ResultDto<bool>> DeleteRol(int Id);
    }
}