using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Data.Entities
{
    public class ShoppingCartItem
    {
       
        public int Id { get; set; }
        public NFT NFT { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
