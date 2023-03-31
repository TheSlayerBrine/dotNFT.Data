using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNFT.Data.Entities
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string TransactionHash { get; set; }
        public User UserSeller { get; set; }
        public User UserBuyer { get; set; }
        public NFT NFTTransferred { get; set; }
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePaidForNFT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
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

