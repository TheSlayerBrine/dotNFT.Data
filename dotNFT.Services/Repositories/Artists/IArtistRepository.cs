using dotNFT.Data.Entities;

namespace dotNFT.Services.Repositories.Artists
{
    public interface IArtistRepository
    {
        void CreateArtist(ArtistDto artistDto);
        IEnumerable<ArtistDto> SearchByName(string searchTerm);
        void DeleteArtist(int artistId);
        List<ArtistDto> GetAll();

        void UpdateArtist(ArtistDto artistDto);
        ArtistDto? GetArtist(int artistId);

    }
}
