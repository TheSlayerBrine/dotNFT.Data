using dotNFT.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNFT.Services.Repositories.NFTs
{
    public class NFTDto
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime MintDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAvailableForSale { get; set; } = true;
        public NFTCategory Category { get; set; }
    }
}
