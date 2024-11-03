
namespace DataLayer.Types;

public interface ITypeDataService
{
    List<TitleType> GetTypes(int pageSize, int pageNumber);

    TitleType? GetTypeById(int typeId);

    int NumberOfTypes();
}