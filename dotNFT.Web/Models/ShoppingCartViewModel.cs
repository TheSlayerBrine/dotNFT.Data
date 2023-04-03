using dotNFT.Data;
using dotNFT.Models;

namespace dotNFT.Web.Models
{
    public class ShoppingCartViewModel
    {
       
        public List<CartItem> ShoppingCartItems { get; set; }
        public int TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
       
    }
}
