using System.Collections.Generic;
using dotNFT.Data.Entities;

namespace dotNFT.Services.Repositories.Transactions
{
    public interface ITransactionRepository
    {
        void AddTransaction(TransactionDto transactionDto);
        List<TransactionDto> GetAllTransactions();
        TransactionDto? GetTransactionById(int id);
        List<TransactionDto> GetTransactionsByUserId(int userId);
        List<TransactionDto> GetTransactionsByNFTId(int nftId);
    }
}