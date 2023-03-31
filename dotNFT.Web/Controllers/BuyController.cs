/*using dotNFT.Services.Repositories.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using dotNFT.Services.Repositories.NFTs;
using dotNFT.Services.Repositories.Users;
using dotNFT.Services.Repositories.Wallets;
using dotNFT.Services;

[HttpPost]
public IActionResult BuyNFT(int nftId, int buyerId)
{
    // Retrieve the NFT object from the NFT repository
    var nft = NFTRepository.GetNFT(nftId);

    // Check if the NFT is available for purchase
    if (!nft.IsAvailableForSale)
    {
        return BadRequest("This NFT is not available for purchase.");
    }

    // Retrieve the buyer's user object from the user repository
    var buyer = UserRepository.GetUser(buyerId);

    // Check if the buyer has enough funds to purchase the NFT
    var purchaseAmount = nft.Price;
    if (buyer.Wallet.Balance < purchaseAmount)
    {
        return BadRequest("You do not have enough funds to purchase this NFT.");
    }

    // Deduct the purchase amount from the buyer's wallet balance and update the wallet object in the database
    using var transactionScope = new TransactionScope();
    buyer.Wallet.Balance -= purchaseAmount;
    WalletRepository.Update(buyer.Wallet);
    UnitOfWork.Save();

    // Update the NFT object's "IsAvailableForSale" property to false and update the NFT object in the database
    nft.IsAvailableForSale = false;
    NFTRepository.UpdateNFT(nft);
    UnitOfWork.Save();

    // Create a new transaction object and save it in the transaction repository
    var transaction = new Transaction
    {
        BuyerId = buyerId,
        NftId = nftId,
        Amount = purchaseAmount,
        TransactionDate = DateTime.Now
    };
    TransactionRepository.Add(transaction);
    UnitOfWork.SaveChangesAsync();

    // Return a success message to the user
    return Ok("You have successfully purchased the NFT.");
}*/