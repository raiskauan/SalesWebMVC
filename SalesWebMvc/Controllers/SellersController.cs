using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using SalesWebMvc.Data;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        
        // GET: SellersController
        public ActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

    }
}
