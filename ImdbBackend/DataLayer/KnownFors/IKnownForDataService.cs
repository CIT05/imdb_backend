
namespace DataLayer.KnownFors
{
    public interface IKnownForDataService
    {
        KnownFor? GetKnownForByTitleId(string titleId);

        KnownFor? GetKnownForByNameId(string nameId);
    }
}
