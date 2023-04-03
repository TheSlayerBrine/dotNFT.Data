using dotNFT.Data.Entities;
using dotNFT.Data;

namespace dotNFT.Services.Repositories.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddTransaction(TransactionDto transactionDto)
        {
            var transaction = new Transaction
            {
                TransactionHash = transactionDto.TransactionHash,
                UserSeller = transactionDto.UserSeller,
                UserBuyer = transactionDto.UserBuyer,
                UserSellerId = transactionDto.UserSellerId,
                UserBuyerId = transactionDto.UserBuyerId,
                NftId = transactionDto.NftId,
                NFTTransferred = transactionDto.NFTTransferred,
                TransactionDate = transactionDto.TransactionDate,
                PricePaidForNFT = transactionDto.PricePaidForNFT,
                GasFee = transactionDto.GasFee
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public List<TransactionDto> GetAllTransactions()
        {
            var transactions = _context.Transactions.ToList();
            var transactionDtos = transactions.Select(t => new TransactionDto
            {
                TransactionHash = t.TransactionHash,
                UserSeller = t.UserSeller,
                UserBuyer = t.UserBuyer,
                NftId = t.NftId,
                UserSellerId = t.UserSellerId,
                UserBuyerId = t.UserBuyerId,
                NFTTransferred = t.NFTTransferred,
                TransactionDate = t.TransactionDate,
                PricePaidForNFT = t.PricePaidForNFT,
                GasFee = t.GasFee
            }).ToList();
            return transactionDtos;
        }

        public TransactionDto? GetTransactionById(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
                return null;
            var transactionDto = new TransactionDto
            {
                TransactionHash = transaction.TransactionHash,
                UserSeller = transaction.UserSeller,
                UserBuyer = transaction.UserBuyer,
                UserSellerId = transaction.UserSellerId,
                UserBuyerId = transaction.UserBuyerId,
                NFTTransferred = transaction.NFTTransferred,
                TransactionDate = transaction.TransactionDate,
                NftId = transaction.NftId,
                PricePaidForNFT = transaction.PricePaidForNFT,
                GasFee = transaction.GasFee
            };
            return transactionDto;
        }

        public List<TransactionDto> GetTransactionsByUserId(int userId)
        {
            var transactions = _context.Transactions.Where(t => t.UserSeller.Id == userId || t.UserBuyer.Id == userId).ToList();
            var transactionDtos = transactions.Select(t => new TransactionDto
            {
                TransactionHash = t.TransactionHash,
                UserSeller = t.UserSeller,
                UserBuyer = t.UserBuyer,
                UserSellerId = t.UserSellerId,
                UserBuyerId = t.UserBuyerId,
                NFTTransferred = t.NFTTransferred,
                TransactionDate = t.TransactionDate,
                NftId = t.NftId,
                PricePaidForNFT = t.PricePaidForNFT,
                GasFee = t.GasFee
            }).ToList();
            return transactionDtos;
        }

        public List<TransactionDto> GetTransactionsByNFTId(int nftId)
        {
            var transactions = _context.Transactions.Where(t => t.NFTTransferred.Id == nftId)
                .Select(t => new TransactionDto
                {
                    TransactionHash = t.TransactionHash,
                    UserSeller = t.UserSeller,
                    UserBuyer = t.UserBuyer,
                    UserSellerId = t.UserSellerId,
                    UserBuyerId = t.UserBuyerId,
                    NFTTransferred = t.NFTTransferred,
                    NftId = t.NftId,
                    TransactionDate = t.TransactionDate,
                    PricePaidForNFT = t.PricePaidForNFT,
                    GasFee = t.GasFee
                })
                .ToList();
            return transactions;
        }
    }
}