using System;
using System.Threading.Tasks;
using dotNFT.Data;
using dotNFT.Services.Repositories;
using dotNFT.Services.Repositories.Artists;
using dotNFT.Services.Repositories.Collections;
using dotNFT.Services.Repositories.NFTs;
using dotNFT.Services.Repositories.Transactions;
using dotNFT.Services.Repositories.Users;
using dotNFT.Services.Repositories.Wallets;

namespace dotNFT.Services
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool _disposed = false;
        private bool _transactionStarted = false;

        public UnitOfWork(AppDbContext dbContext,
            IArtistRepository artistRepository,
            ICollectionRepository collectionRepository,
            INFTRepository nftRepository,
            ITransactionRepository transactionRepository,
            IUserRepository userRepository,
            IWalletRepository walletRepository)
        {
            _context = dbContext;
            Artists = artistRepository;
            Collections = collectionRepository;
            NFTs = nftRepository;
            Transactions = transactionRepository;
            Users = userRepository;
            Wallets = walletRepository;
        }

        public IArtistRepository Artists { get; }
        public ICollectionRepository Collections { get; }
        public INFTRepository NFTs { get; }
        public ITransactionRepository Transactions { get; }
        public IUserRepository Users { get; }
        public IWalletRepository Wallets { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transactionStarted)
            {
                throw new InvalidOperationException("Transaction has already been started.");
            }
            _transactionStarted = true;
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (!_transactionStarted)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }
            _transactionStarted = false;
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            if (!_transactionStarted)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }
            _transactionStarted = false;
            await _context.Database.RollbackTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}