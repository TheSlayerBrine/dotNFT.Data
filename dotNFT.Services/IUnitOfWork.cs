using dotNFT.Services.Repositories.Artists;
using dotNFT.Services.Repositories.Collections;
using dotNFT.Services.Repositories.NFTs;
using dotNFT.Services.Repositories.Transactions;
using dotNFT.Services.Repositories.Users;
using dotNFT.Services.Repositories.Wallets;

namespace dotNFT.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IArtistRepository Artists { get; }
        ICollectionRepository Collections { get; }
        INFTRepository NFTs { get; }
        ITransactionRepository Transactions { get; }
        IUserRepository Users { get; }
        IWalletRepository Wallets { get; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
