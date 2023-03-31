using dotNFT.Data.Entities;
using dotNFT.Services.Repositories.Collections;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                NFTTransferred = transaction.NFTTransferred,
                TransactionDate = transaction.TransactionDate,
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
                NFTTransferred = t.NFTTransferred,
                TransactionDate = t.TransactionDate,
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
    NFTTransferred = t.NFTTransferred,
    TransactionDate = t.TransactionDate,
    PricePaidForNFT = t.PricePaidForNFT,
    GasFee = t.GasFee
})
.ToList();
            return transactions;
        }
    }
}