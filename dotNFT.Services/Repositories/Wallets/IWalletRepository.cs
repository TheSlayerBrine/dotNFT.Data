using dotNFT.Data.Entities;
using dotNFT.Data;
using dotNFT.Services.Repositories.NFTs;

namespace dotNFT.Services.Repositories.Wallets
{
    public interface IWalletRepository
    {
        void CreateWallet(WalletDto WalletDto, int userId);
        IEnumerable<WalletDto> SearchByAddress(string searchTerm);
        void DeleteWallet(int Id);
        void UpdateWallet(WalletDto WalletDto);
        //List<NFTDto> GetAllNFTs();
        WalletDto? GetWallet(int Id);
        
    }
}
