namespace Group.Salto.ServiceLibrary.Common.Dtos.Notification
{
    public class EmailSmtpConfigurationDto
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public bool SmtpEnableSSL { get; set; }
        public string EmailFrom { get; set; }
    }
}