using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNFT.Data.Entities;

namespace dotNFT.Services.Repositories.ShoppingCartItems
{
    public interface IShoppingCartRepository
    {
        ShoppingCartItem GetCart(IServiceProvider services, int cartId);
    }
}