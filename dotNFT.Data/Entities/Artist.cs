using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace dotNFT.Data.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string ProfilePictureURL { get; set; }
        public string ArtistName { get; set; }
        public string Bio { get; set; }

        public User User { get; set; }

        //Relationships
        public IList<NFT> NFTs { get; set; }
    }
}
