using System.Collections.Generic;

namespace dotNFT.Data.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}