using dotNFT.Data.Entities;
using System.Xml.Linq;

namespace dotNFT.Services.Repositories.Collections
{
    public interface ICollectionRepository
    {
        void CreateCollection(CollectionDto CollectionDto);
        IEnumerable<CollectionDto> SearchByName(string searchTerm);
        void DeleteCollection(int CollectionId);
        List<CollectionDto> GetAll();

        void UpdateCollection(CollectionDto CollectionDto);
        CollectionDto? GetCollection(int CollectionId);
    }
}
