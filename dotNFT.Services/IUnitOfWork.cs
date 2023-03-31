using dotNFT.Services.Repositories.Artists;
using dotNFT.Services.Repositories.Collections;
using dotNFT.Services.Repositories.NFTs;
using dotNFT.Services.Repositories.ShoppingCartItems;
using dotNFT.Services.Repositories.Transactions;
using dotNFT.Services.Repositories.Users;
using dotNFT.Services.Repositories.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IArtistRepository Artists { get; }
        ICollectionRepository Collections { get; }
        INFTRepository NFTs { get; }
        IShoppingCartRepository ShoppingCartItems { get; }
        ITransactionRepository Transactions { get; }
        IUserRepository Users { get; }
        IWalletRepository Wallets { get; }
        Task<int> SaveChangesAsync();
    }
}
