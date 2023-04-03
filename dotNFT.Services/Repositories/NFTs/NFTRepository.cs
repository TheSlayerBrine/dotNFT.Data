using dotNFT.Data.Entities;
using dotNFT.Services.Repositories.NFTs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNFT.Data;
using Microsoft.EntityFrameworkCore;
using dotNFT.Services.Connections;
namespace dotNFT.Services.Repositories.NFTs
{
    public class NFTRepository : INFTRepository
    {
        private readonly AppDbContext _context;

        public NFTRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateNFT(NFTDto nftDto, int walletId)
        {
            if (nftDto == null) throw new ArgumentNullException(nameof(nftDto));
            if (string.IsNullOrEmpty(nftDto.Name)) throw new ArgumentException($"{nameof(nftDto.Name)} cannot be null or empty.");

            if (_context.NFTs.Any(n => n.Name == nftDto.Name))
            {
                throw new Exception("Cannot insert a new NFT with an existing name.");
            }

            var nftEntity = new NFT
            {
                Name = nftDto.Name,
                Id = nftDto.Id,
                Description = nftDto.Description,
                Price   = nftDto.Price,
                ImageURL = nftDto.ImageURL,
                MintDate    = nftDto.MintDate,
                EndDate = nftDto.EndDate,
                Category = nftDto.Category,
               
            };
          

            var walletService = new WalletService(_context);

            walletService.AddNFT(walletId, nftEntity);
            _context.SaveChanges();


        }

        public void DeleteNFT(int nftId)
        {
            if (nftId <= 0) throw new ArgumentOutOfRangeException(nameof(nftId));

            var nftToDelete = _context.NFTs.Find(nftId);

            if (nftToDelete != null)
            {
                _context.NFTs.Remove(nftToDelete);
            }
        }

        public List<NFTDto> GetAll()
        {
            return _context.NFTs
                .Select(n => new NFTDto
                {
                    Name = n.Name,
                    Description = n.Description,
                    Price = n.Price,
                    ImageURL = n.ImageURL,
                    MintDate = n.MintDate,
                    EndDate = n.EndDate,
                    Category = n.Category,
                    
                })
                .ToList();
        }

        public NFTDto? GetNFT(int nftId)
        {
            if (nftId <= 0) throw new ArgumentOutOfRangeException(nameof(nftId));

            var nft = _context.NFTs.SingleOrDefault(n => n.Id == nftId);

            if (nft == null) return null;

            var nftDto = new NFTDto
            {
                Name = nft.Name,
                Description = nft.Description,
                Price = nft.Price,
                ImageURL = nft.ImageURL,
                MintDate = nft.MintDate,
                EndDate = nft.EndDate,
                Category = nft.Category,
               

            };

            return nftDto;
        }


        public IEnumerable<NFTDto> SearchByName(string searchTerm)
        {
            return _context.NFTs
               .Where(n => n.Name.Contains(searchTerm))
               .Select(n => new NFTDto
               {
                   Name = n.Name,
                   Description = n.Description,
                   Price = n.Price,
                   ImageURL = n.ImageURL,
                   MintDate = n.MintDate,
                   EndDate = n.EndDate,
                   Category = n.Category,
                 
               })
               .ToList();
        }

        public void UpdateNFT(NFTDto nftDto)
        {
            if (nftDto == null) throw new ArgumentNullException(nameof(nftDto));
            if (string.IsNullOrEmpty(nftDto.Name)) throw new ArgumentException($"{nameof(nftDto.Name)} cannot be null or empty.");

            var nftToUpdate = _context.NFTs.Find(nftDto.Id);
            if (nftToUpdate == null)
            {
                throw new Exception("The nft has not been found");
            }

            nftToUpdate.Name = nftDto.Name;
            nftToUpdate.Description = nftDto.Description;
            nftToUpdate.Price = nftDto.Price;
            nftToUpdate.ImageURL = nftDto.ImageURL;
            nftToUpdate.MintDate = nftDto.MintDate;
            nftToUpdate.EndDate = nftDto.EndDate;
            nftToUpdate.Category = nftDto.Category;


            if (!nftToUpdate.IsAvailableForSale)
            {
                throw new Exception("The NFT is not available for sale");
            }

            nftToUpdate.IsAvailableForSale = false;
            _context.SaveChanges();
        }

    }
}
