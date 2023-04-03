using dotNFT.Data.Entities;
using dotNFT.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace dotNFT.Data.Entities
{
    public class WalletDto
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Address { get; set; }
        public Network Network { get; set; }
        public decimal Balance { get; set; }
        public string SecretPassPhrase { get; set; }
        public IList<NFT> NFTs { get; set; }
    }
}
