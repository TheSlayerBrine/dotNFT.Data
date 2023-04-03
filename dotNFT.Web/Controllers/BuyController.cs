using Microsoft.AspNetCore.Mvc;
using dotNFT.Services.Repositories.NFTs;
using dotNFT.Services.Repositories.Transactions;
using dotNFT.Services.Repositories.Users;
using dotNFT.Services.Repositories.Wallets;
using dotNFT.Services;
using dotNFT.Data.Entities;

namespace dotNFT.Web.Controllers
{
    public class BuyController : Controller
    {
        private readonly INFTRepository _nftRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BuyController(INFTRepository nftRepository, IUserRepository userRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _nftRepository = nftRepository;
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult BuyNFT(int nftId, int buyerId)
        {
            // Retrieve the NFT object from the NFT repository
            var nft = _nftRepository.GetNFT(nftId);

            // Check if the NFT is available for purchase
            if (!nft.IsAvailableForSale)
            {
                return BadRequest("This NFT is not available for purchase.");
            }

            // Retrieve the buyer's user object from the user repository
            var buyer = _userRepository.GetUser(buyerId);

            // Check if the buyer has enough funds to purchase the NFT
            var purchaseAmount = nft.Price;
            if (buyer.Wallet.Balance < purchaseAmount)
            {
                return BadRequest("You do not have enough funds to purchase this NFT.");
            }

            // Deduct the purchase amount from the buyer's wallet balance and update the wallet object in the database
            buyer.Wallet.Balance -= purchaseAmount;
            var nftEntity = new NFT
            {
                Name = nft.Name,
                Id = nft.Id,
                Description = nft.Description,
                Price = nft.Price,
                ImageURL = nft.ImageURL,
                MintDate = nft.MintDate,
                EndDate = nft.EndDate,
                Category = nft.Category,
            };
            buyer.Wallet.NFTs.Add(nftEntity);
            var walletDto = new WalletDto
            {
                Id = nft.Id,
                Name = nft.Name,
                Address  = buyer.Wallet.Address,
                Balance = buyer.Wallet.Balance,
                NFTs = buyer.Wallet.NFTs,
            };
            _walletRepository.UpdateWallet(walletDto);
            _unitOfWork.SaveChanges();

            // Update the NFT object's "IsAvailableForSale" property to false and update the NFT object in the database
            nft.IsAvailableForSale = false;
            _nftRepository.UpdateNFT(nft);
            _unitOfWork.SaveChanges();

            // Create a new transaction object and save it in the transaction repository
            var transaction = new TransactionDto
            {
                UserBuyerId = buyerId,
                NftId = nftId,
                PricePaidForNFT = nft.Price,
                TransactionDate = DateTime.Now
            };
            _transactionRepository.AddTransaction(transaction);
            _unitOfWork.SaveChanges();

            // Return a success message to the user
            return Ok("You have successfully purchased the NFT.");
        }
    }
}