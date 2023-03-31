using dotNFT.Web.Models.Users;

namespace dotNFT.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string TransactionHash { get; set; }
        public UserViewModel UserSeller { get; set; }
        public UserViewModel UserBuyer { get; set; }
        public NFTViewModel NFTTransferred { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal PricePaidForNFT { get; set; }
        public decimal GasFee { get; set; }
        public decimal TotalPricePaid
        {
            get
            {
                return GasFee + PricePaidForNFT;
            }
        }

    }
}
