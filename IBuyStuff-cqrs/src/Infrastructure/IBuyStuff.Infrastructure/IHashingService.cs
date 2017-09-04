namespace IBuyStuff.Infrastructure
{
    public interface IHashingService
    {
        bool Validate(string clearPassword, string hash);
        string Hash(string clearPassword);
    }
}