using dotNFT.Data;
using dotNFT.Data.Entities;
using dotNFT.Services.Generators;

namespace dotNFT.Services.Repositories.Wallets
{

    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext _context;


        public WalletRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateWallet(WalletDto WalletDto, int userId)
        {
            if (WalletDto == null) throw new ArgumentNullException(nameof(WalletDto));
            if (string.IsNullOrEmpty(WalletDto.Name)) throw new ArgumentException($"{nameof(WalletDto.Name)} cannot be null or empty.");

            // Generate a new fake crypto address
            var WalletEntity = new Wallet
            {
                Name = WalletDto.Name,
                Id = WalletDto.Id,
                Network = WalletDto.Network,
                Balance = WalletDto.Balance,
                Address = PassphraseGenerator.Generate(),
                SecretPassPhrase = GenerateCryptoAddress.GenerateFakeCryptoAddress(),
                UserId = userId // set the UserId to the ID of the user who is creating the wallet
            };

            _context.Wallets.Add(WalletEntity);
            _context.SaveChanges();
        }

        public void DeleteWallet(int WalletId)
        {
            if (WalletId <= 0) throw new ArgumentOutOfRangeException(nameof(WalletId));

            var WalletToDelete = _context.Wallets.Find(WalletId);

            if (WalletToDelete != null)
            {
                _context.Wallets.Remove(WalletToDelete);
            }
        }

        public List<WalletDto> GetAllNFTs()
        {
            return _context.NFTs
                .Select(w => new WalletDto
                {


                })
                .ToList();
        }

        public WalletDto? GetWallet(int WalletId)
        {
            if (WalletId <= 0) throw new ArgumentOutOfRangeException(nameof(WalletId));

            var Wallet = _context.Wallets.SingleOrDefault(u => u.Id == WalletId);

            if (Wallet == null) return null;

            var WalletDto = new WalletDto
            {
                Name = Wallet.Name,
                Id = Wallet.Id,
                Network = Wallet.Network,
                Balance = Wallet.Balance,
            };

            return WalletDto;
        }


        public IEnumerable<WalletDto> SearchByAddress(string searchTerm)
        {
            return _context.Wallets
               .Where(w => w.Address.Contains(searchTerm))
               .Select(w => new WalletDto
               {
                   Name = w.Name,
                   Id = w.Id,
                   Network = w.Network,
                   Balance = w.Balance,
               })
               .ToList();
        }

        public void UpdateWallet(WalletDto WalletDto)
        {
            if (WalletDto == null) throw new ArgumentNullException(nameof(WalletDto));
            if (string.IsNullOrEmpty(WalletDto.Name)) throw new ArgumentException($"{nameof(WalletDto.Name)} cannot be null or empty.");

            var WalletToUpdate = _context.Wallets.Find(WalletDto.Address);
            if (WalletToUpdate == null)
            {
                throw new Exception("The Wallet has not been found");
            }

            WalletToUpdate.Name = WalletDto.Name;

        }
    }
}