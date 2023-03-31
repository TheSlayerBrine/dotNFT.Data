
using dotNFT.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using dotNFT.Services.Repositories.ShoppingCartItems;
using dotNFT.Data.Entities;

namespace dotNFT.Services.Repositories.ShoppingCartItems
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;

        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }

        public ShoppingCartItem GetCart(IServiceProvider services, int cartId)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            cartId = session.GetInt32("CartId") ?? new Random().Next(1, int.MaxValue);
            session.SetInt32("CartId", cartId);
            return new ShoppingCartItem { Id = cartId };
        }
    }
}