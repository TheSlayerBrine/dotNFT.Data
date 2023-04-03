using System.Collections.Generic;

namespace dotNFT.Data.Entities
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public string UserId { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}