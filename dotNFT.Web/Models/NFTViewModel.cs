using dotNFT.Data;
using dotNFT.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNFT.Models
{
    public class NFTViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime MintDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAvailableForSale { get; set; } = true;

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        //Collection
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }

        public Wallet Wallet { get; set; }
        public NFTCategory Category { get; set; }
    }
}
