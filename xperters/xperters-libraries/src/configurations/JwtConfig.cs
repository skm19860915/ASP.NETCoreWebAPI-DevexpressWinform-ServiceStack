namespace xperters.configurations
{

    public class JwtConfig
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}