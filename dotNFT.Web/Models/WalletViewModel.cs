using dotNFT.Data.Enums;

namespace dotNFT.Models
{
    public class WalletViewModel
    {

        public string Address { get; set; }
        public Network Network { get; set; }
        public decimal Balance { get; set; }

        public NFTViewModel NFTs { get; set; }

    }
}
