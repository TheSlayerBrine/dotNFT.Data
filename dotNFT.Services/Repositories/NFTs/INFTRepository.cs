using System.Collections.Generic;

namespace dotNFT.Services.Repositories.NFTs
{
    public interface INFTRepository
    {
        void CreateNFT(NFTDto nftDto, int walletId);

        IEnumerable<NFTDto> SearchByName(string searchTerm);

        void DeleteNFT(int nftId);

        List<NFTDto> GetAll();

        void UpdateNFT(NFTDto nftDto);

        NFTDto? GetNFT(int nftId);
    }
}