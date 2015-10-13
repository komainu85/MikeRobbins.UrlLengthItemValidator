namespace MikeRobbins.UrlLengthItemValidator.Contracts
{
    public interface IUrlChecker
    {
        bool IsValidLength(int itemUrlLength);
        int MaxLengthAllowed();
    }
}