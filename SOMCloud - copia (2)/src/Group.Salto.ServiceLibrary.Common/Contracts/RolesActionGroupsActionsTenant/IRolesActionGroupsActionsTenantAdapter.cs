namespace Group.Salto.ServiceLibrary.Common.Contracts.RolesActionGroupsActionsTenant
{
    public interface IRolesActionGroupsActionsTenantAdapter
    {
        bool SetCacheRolesActionGroupsActionsByUserId(int userId, string customerId);
    }
}