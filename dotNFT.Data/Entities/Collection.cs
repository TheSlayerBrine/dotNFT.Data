using System.ComponentModel.DataAnnotations;

namespace dotNFT.Data.Entities
{
    public class Collection 
    {
        
        public int Id { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //Relationships
        public List<NFT> NFTs { get; set; }
    }
}
