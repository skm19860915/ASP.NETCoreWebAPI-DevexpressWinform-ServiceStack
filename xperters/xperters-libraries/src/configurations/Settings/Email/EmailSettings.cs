
namespace xperters.configurations.Settings.Email
{
   public class EmailSettings
    {
        public string ApiKey { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SentFromName { get; set; }
        public string SentFromEmail { get; set; }
        public string PopServer { get; set; }
        public string PopPort { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPortSsl { get; set; }
        public int SmtpPortTls { get; set; }
    }
}
