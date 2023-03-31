using dotNFT.Data;
using dotNFT.Data.Entities;
namespace dotNFT.Web.Models
{
    public class ShoppingCart
    {
        public string  CartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

       
    }
}
