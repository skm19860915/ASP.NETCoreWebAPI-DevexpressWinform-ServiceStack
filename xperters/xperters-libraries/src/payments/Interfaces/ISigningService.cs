namespace xperters.payments.Interfaces
{
    public interface ISigningService
    {
        string  SignData(string data);
        bool VerifyData(string expected, string signedBase64);
    }
}