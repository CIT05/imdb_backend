
namespace DataLayer.KnownFors
{
    public interface IKnownForDataService
    {
        List<KnownFor> GetKnownForByTitleIds(List<string> titleIds);

        List<KnownFor> GetKnownForByNameId(string nameId);
    }
}
