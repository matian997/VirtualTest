namespace VirtualTest.Services.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string text);
        string Dencrypt(string text);
    }
}
