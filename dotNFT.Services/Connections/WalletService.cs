    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNFT.Data;
using dotNFT.Data.Entities;
using dotNFT.Services.Repositories.NFTs;
using Microsoft.EntityFrameworkCore;

namespace dotNFT.Services.Connections
{
    public class WalletService
    {
        private readonly AppDbContext _context;

        public WalletService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public void AddNFT(int  walletId, NFT nft)
        {
            var wallet = _context.Wallets.FirstOrDefault(w => w.Id == walletId);
            if (wallet == null)
            {
                // handle case where wallet does not exist
            }

            _context.NFTs.Add(nft);
            _context.SaveChanges();
        }
    }
}
