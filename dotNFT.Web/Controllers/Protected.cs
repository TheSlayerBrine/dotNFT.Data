using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNFT.Web.Controllers
{
    [Authorize]
    public class ProtectedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
