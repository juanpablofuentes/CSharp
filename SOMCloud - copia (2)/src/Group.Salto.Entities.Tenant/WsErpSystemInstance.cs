namespace Group.Salto.Entities.Tenant
{
    public class WsErpSystemInstance
    {
        public int ErpSystemInstanceId { get; set; }
        public string WebServiceIpAddress { get; set; }
        public string WebServiceUser { get; set; }
        public string WebServicePwd { get; set; }
        public string WebServiceMethod { get; set; }

        public ErpSystemInstance ErpSystemInstance { get; set; }
    }
}
