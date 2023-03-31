using dotNFT.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Services.Repositories.ShoppingCartItems.Dto
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public NFT NFT { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
