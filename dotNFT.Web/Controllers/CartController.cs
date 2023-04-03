using dotNFT.Data;
using dotNFT.Data.Entities;
using dotNFT.Models;
using dotNFT.Utilities;
using dotNFT.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var cartVM = new ShoppingCartViewModel()
            {
                ShoppingCartItems = cart,
                TotalPrice = cart.Sum(x => x.Amount * x.Price),
                TotalAmount = cart.Sum(x => x.Amount),
            };

            return View(cartVM);
        }

        public async Task<IActionResult> Add(long id)
        {
            NFT nft = await _context.NFTs.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.NftId == id).FirstOrDefault();

            if (cartItem == null)
            {
                var newItem = new CartItem
                {
                    NftId = nft.Id,
                    NftName = nft.Name,
                    Amount = 1,
                    Price = nft.Price
                };
                cart.Add(newItem);
            }
            else
            {
                cartItem.Amount += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The nft has been added!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.NftId == id).FirstOrDefault();

            if (cartItem.Amount > 1)
            {
                --cartItem.Amount;
            }
            else
            {
                cart.RemoveAll(p => p.NftId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The nft has been removed!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.NftId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The nft has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
    }
}
